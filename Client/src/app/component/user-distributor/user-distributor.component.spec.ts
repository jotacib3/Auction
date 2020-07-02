import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserDistributorComponent } from './user-distributor.component';

describe('UserDistributorComponent', () => {
  let component: UserDistributorComponent;
  let fixture: ComponentFixture<UserDistributorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserDistributorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserDistributorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
