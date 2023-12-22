import { Injectable, Injector, NgZone } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { AppComponentBase } from '@shared/common/app-component-base';
import { UserAuction } from '@shared/service-proxies/service-proxies';

@Injectable({
    providedIn: 'root'
})
export class AuctionService extends AppComponentBase {
    chatHub: HubConnection;
    isChatConnected = false;
    userAuction: UserAuction;
    amountPresent;
    amountJumnpMax;
    amountJumnpMin;
    constructor(injector: Injector, public _zone: NgZone) {
        super(injector);
    }

    configureConnection(connection): void {
        // Set the common hub
        this.chatHub = connection;

        // Reconnect loop
        let reconnectTime = 5000;
        let tries = 1;
        let maxTries = 8;
        function start() {
            return new Promise(function (resolve, reject) {
                if (tries > maxTries) {
                    reject();
                } else {
                    connection
                        .start()
                        .then(resolve)
                        .then(() => {
                            reconnectTime = 5000;
                            tries = 1;
                        })
                        .catch(() => {
                            setTimeout(() => {
                                start().then(resolve);
                            }, reconnectTime);
                            reconnectTime *= 2;
                            tries += 1;
                        });
                }
            });
        }

        // Reconnect if hub disconnects
        connection.onclose((e) => {
            this.isChatConnected = false;

            if (e) {
                abp.log.debug('Chat connection closed with error: ' + e);
            } else {
                abp.log.debug('Chat disconnected');
            }

            start().then(() => {
                this.isChatConnected = true;
            });
        });

        // Register to get notifications
        this.registerChatEvents(connection);
    }

    registerChatEvents(connection): void {
        // connection.on('updateAmountOfAuction', (message) => {
        //     abp.event.trigger('app.chat.updateAuction', message);
        // });

        connection.on("updateAuction", (amountPresent, amountJumnpMin, amountJumnpMax) => {
            this.amountPresent = amountPresent;
            this.amountJumnpMin = amountJumnpMin;
            this.amountJumnpMax = amountJumnpMax;
            abp.event.trigger('app.chat.updateAmountAuction', this.amountPresent, this.amountJumnpMin, this.amountJumnpMax);
        })
    }
    getAmountPresent() {
        return this.amountPresent;
    }
    getAmountJumnpMin() {
        return this.amountJumnpMin;
    }
    getAmountJumnpMax() {
        return this.amountJumnpMax;
    }

    updateAuction(amountPresent:number, auctionId, isPublic:boolean, callback): void {
        if (!this.isChatConnected) {
            if (callback) {
                callback();
            }

            abp.notify.warn(this.l('Chưa có kết nối'));
            return;
        }
        this.chatHub
             .invoke('updateAmountOfAuction', amountPresent, parseInt(auctionId), isPublic,abp.session.userId)
            //.invoke('updateAmountOfAuction',1)
            .then((result) => {
                this.notify.success("Đấu giá thành công");
                if (result) {
                    abp.notify.warn(result);
                }

                if (callback) {
                    callback();
                }
            })
            .catch((error) => {
                abp.log.error(error);

                if (callback) {
                    callback();
                }
            });
    }

    init(): void {
        this._zone.runOutsideAngular(() => {
            abp.signalr.connect();
            abp.signalr
                .startConnection(abp.appPath + 'update-amount-auction', (connection) => {
                    this.configureConnection(connection);
                })
                .then(() => {
                    abp.event.trigger('app.chat.connected');
                    this.isChatConnected = true;
                });
        });
    }
}
