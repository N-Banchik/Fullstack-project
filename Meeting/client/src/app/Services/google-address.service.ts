import { Injectable } from '@angular/core';
import { Address } from 'ngx-google-places-autocomplete/objects/address';

@Injectable({
  providedIn: 'root',
})
export class GoogleAddressService {
  addressToString: {
    country: string | undefined;
    city: string | undefined;
  } = {} as any;

  constructor() {}

  GetAddressToString(address: Address) {
    this.addressToString.city = address.address_components.find((x) =>
      x.types.includes('locality')
    )?.long_name;

    if (
      (this.addressToString.country = address.address_components.find((x) =>
        x.types.includes('"administrative_area_level_1"')
      )?.long_name) === undefined) 
      {
      this.addressToString.country = address.address_components.find((x) =>
        x.types.includes('country')
      )?.long_name;
      return this.addressToString;
    }

    this.addressToString.country =
      address.address_components.find((x) =>
        x.types.includes('"administrative_area_level_1"')
      )?.long_name +
      ' ' +
      address.address_components.find((x) => x.types.includes('country'))
        ?.long_name;
    return this.addressToString;
  }

  GetAddressToEvent(address: Address) {
    return address.formatted_address;
  }

  
}
