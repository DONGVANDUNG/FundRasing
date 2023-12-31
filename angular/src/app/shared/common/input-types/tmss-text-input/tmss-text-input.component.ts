import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Component, Input, Output, EventEmitter, forwardRef, ViewChild, ElementRef, Self, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'tmss-text-input',
  templateUrl: './tmss-text-input.component.html',
  styleUrls: ['./tmss-text-input.component.less'],
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => TmssTextInputComponent),
    multi: true
  }]
})
export class TmssTextInputComponent extends AppComponentBase implements ControlValueAccessor {
  @Input() value;
  @Input() className: string = '';
  @Input() addOnMinWidth: string = '';
  @Input() text: string = '';
  @Input() isRequired: boolean = false;
  @Input() isValidate: boolean = false;
  @Input() placeholder: string = '';
  @Input() disable: boolean = false;
  @Input() hideInput: boolean = false;
  @Input() showSearch: boolean = false;
  @Input() rows: any;
  @Input() showModal: boolean = false;
  @Input() isReadonly: boolean = false;
  @Input() isDisabled: boolean = false;
  @Input() showCaret: boolean = false;
  @Input() showTrash: boolean = false;
  @Input() showUpload: boolean = false;
  @Input() type: string = 'text';
  @Input() maxLength: number = 0;

  @Output() onSearch = new EventEmitter();
  @Output() onChoose = new EventEmitter();
  @Output() onRefresh = new EventEmitter();
  @Output() onClickInput = new EventEmitter();
  @Output() keyup = new EventEmitter();
  private onChange: Function = new Function();
  @ViewChild('input') input!: ElementRef;
  constructor(
		injector: Injector) {
		super(injector);
	}

  changeValue(event: any) {
    const value = event ? (event.target as HTMLInputElement).value : null;
    if (event.key === 'Enter') this.onClickInput.emit(value);

    if (value === '') {
      this.value = '';
    } else {
      this.value = value ?? '';
    }

    if (typeof this.onChange === 'function') {
      this.onChange(value ?? '');
    }
  }

  writeValue(val: any) {
    this.value = val ?? '';
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
  }

  openModal(event: any) {
    const value = event ? (event.target as HTMLInputElement).value : null;
    if (event.key === 'Enter') this.onClickInput.emit(value);
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  search() {
    this.onSearch.emit(this.value);
  }

  onClickInputValue() {
    this.onClickInput.emit('');
  }

  refresh() {
    this.onRefresh.emit();
  }

  chooseFile() {
    this.onChoose.emit();
  }
}
