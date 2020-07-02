import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '../../../../../../node_modules/@angular/forms';

@Component({
  selector: 'app-catalog-edit',
  templateUrl: './catalog-edit.component.html',
  styleUrls: ['./catalog-edit.component.css']
})
export class CatalogEditComponent implements OnInit {

  validateForm: FormGroup;
  @Output() editItemEvent = new EventEmitter<any>();
  @Input() valueInput: any;

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
    const val: any = Object.assign({}, this.validateForm.value);
    val.id = this.valueInput.id;
    this.editItemEvent.emit(val);
  }

  constructor( private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      nombre: [ null, [ Validators.required ] ]
    });
  }

}
