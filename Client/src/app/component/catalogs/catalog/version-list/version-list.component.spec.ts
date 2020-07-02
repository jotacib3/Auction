/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { VersionListComponent } from './version-list.component';

describe('VersionListComponent', () => {
  let component: VersionListComponent;
  let fixture: ComponentFixture<VersionListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VersionListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VersionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
