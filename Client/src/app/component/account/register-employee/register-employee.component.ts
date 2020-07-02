import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '../../../../../node_modules/@angular/forms';
import { ActivatedRoute, Router } from '../../../../../node_modules/@angular/router';
import { NzNotificationService, UploadFile } from '../../../../../node_modules/ng-zorro-antd';
import { AuthService } from '../../../services/auth.service';
import { RepositoryService } from '../../../services/repository.service';
import { City } from '../../../models/city';
import { State } from '../../../models/state';
import { EmployeeData } from '../../../models/employeeData';
import { map } from '../../../../../node_modules/rxjs-compat/operator/map';
import { RegisterEmployee } from '../../../models/register-employee';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { UserService } from '../../../services/user.service';
@Component({
  selector: 'app-register-employee',
  templateUrl: './register-employee.component.html',
  styleUrls: ['./register-employee.component.css']
})
export class RegisterEmployeeComponent implements OnInit , OnDestroy {

  cities: Array<City>;
  states: Array<State>;
  private cityUrl = 'cities';
  private stateUrl = 'states';
  private UserId: any;
  private fil: File;
  showUploadList = {
    showPreviewIcon: true,
    showRemoveIcon: true,
    hidePreviewIconInNonImage: true
  };
  fileList = [
  ];
  previewImage: string | undefined = '';
  previewVisible = false;

  validateForm: FormGroup;

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
    const val: any = Object.assign({}, this.validateForm.value);
    const regEmplModel: EmployeeData = new EmployeeData();
    regEmplModel.cellNumber = val.phoneNumber;
    regEmplModel.cityId = val.cityId;
    regEmplModel.name = val.name;
    regEmplModel.colony = val.colony;
    regEmplModel.fatherSurname = val.fatherSurname;
    regEmplModel.fixNumber = val.fixNumber;
    regEmplModel.insideNumber = val.insideNumber;
    regEmplModel.motherSurname = val.motherSurname;
    regEmplModel.noEmployee = val.noEmployee;
    regEmplModel.outsideNumber = val.outsideNumber;
    regEmplModel.stateId = val.stateId;
    regEmplModel.street = val.street;
    regEmplModel.zipCode = val.zipCode;
    const registerEmployeeModel: RegisterEmployee = new RegisterEmployee();
    registerEmployeeModel.employee = regEmplModel;
    registerEmployeeModel.email = val.email;
    registerEmployeeModel.password = val.password;
    registerEmployeeModel.confirmPassword = val.checkPassword;
    registerEmployeeModel.role = 'EMPLOYEE';
    registerEmployeeModel.userName = val.email;
    this.auth.registerEmployee(registerEmployeeModel).subscribe(data => {
      this.UserId = data.text();
      this.userService.postPhoto(`photo/employee/${this.UserId}`,
        // tslint:disable-next-line:no-shadowed-variable
        this.fileList[0].originFileObj).subscribe(data => {
            this.fileList = [];
          });
    }, error => {
      this.notification.error('Error de registro.', 'Faltan campos por completar.');
    });

  }
  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.validateForm.controls.checkPassword.updateValueAndValidity());
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router,
              private notification: NzNotificationService, public auth: AuthService,
              private repo: RepositoryService, private userService: UserService) {
  }

  handlePreview = (file: UploadFile) => {
    this.previewImage = file.url || file.thumbUrl;
    this.previewVisible = true;
  }

  ngOnInit(): void {
    document.body.className = 'login-background';
    this.validateForm = this.fb.group({
      noEmployee: [null, [Validators.required, Validators.maxLength(12)]],
      name: [null, [Validators.required, Validators.maxLength(25)]],
      fatherSurname: [null, [Validators.required, Validators.maxLength(20)]],
      motherSurname: [null, [Validators.required, Validators.maxLength(20)]],
      fixNumber: [null, [Validators.required]],
      phoneNumber: [null, [Validators.required]],
      street: [null, [Validators.required]],
      colony: [null, [Validators.required]],
      outsideNumber: [null],
      insideNumber: [null],
      zipCode: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(5)]],
      cityId: [null, Validators.required],
      stateId: [null, Validators.required],
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]],
    });

   this.loadData();
  }
  loadData() {
    this.repo.getReg(this.cityUrl).subscribe(data => {this.cities = data as Array<City>; });
    this.repo.getReg(this.stateUrl).subscribe(data => {this.states = data as Array<State>; });
  }

  ngOnDestroy() {
    document.body.className = '';
  }

}


