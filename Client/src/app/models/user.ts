import { EmployeeData } from './employeeData';
import { State } from './state';
import { City } from './city';

export class User {
    enabled: boolean;
    email: string;
    phoneNumber: string;
    id: string;
    userName: string;
    state: State;
    city: City;
    employeeData: EmployeeData;
}
