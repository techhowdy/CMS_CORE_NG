import { Address } from './address';

export interface Profile {
    userid: string;
    userRole: string;
    email: string;
    username: string;
    phone: string;
    birthday: string;
    gender: string;
    displayname: string;
    shippingAddress: Address;
    billingAddress: Address;
    profpicfile: string;
    firstname: string;
    lastname: string;
    middlename: string;
    isTwoFactorOn: boolean;
    isPhoneVerified: boolean;
    isEmailVerified: boolean;
    isTermsAccepted: boolean;
}
