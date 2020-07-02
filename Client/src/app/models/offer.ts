import { Vehicle } from './vehicle';
import { User } from './user';

export class Offer {
    id: number;
    price: number;
    description: string;
    created: Date;
    publicationId: number;
    publication: Vehicle;
    userId: string;
    employee: User;
    enabled: boolean;
}
