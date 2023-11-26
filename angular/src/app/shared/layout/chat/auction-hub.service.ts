import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuctionService {
  private hubConnection: signalR.HubConnection;

  private auctionReceived = new Subject<any>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('/update-amount-auction')
      .build();

    this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('EmitAmountAuction', (amountAutionData) => {
      this.auctionReceived.next(amountAutionData);
    });
  }
  getAmountAution(): Subject<any> {
    return this.auctionReceived;
  }
}
