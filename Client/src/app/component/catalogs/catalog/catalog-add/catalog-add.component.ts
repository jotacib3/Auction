import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-catalog-add',
  templateUrl: './catalog-add.component.html',
  styleUrls: ['./catalog-add.component.css']
})
export class CatalogAddComponent implements OnInit {

  validateForm: FormGroup;
  @Output() addItemEvent = new EventEmitter<any>();

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
    this.addItemEvent.emit(this.validateForm.value);
  }

  constructor( private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.validateForm = this.fb.group({
      nombre: [ null, [ Validators.required ] ]
    });
  }

}
