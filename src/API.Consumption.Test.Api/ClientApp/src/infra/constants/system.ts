import { environment } from 'src/environments/environment';

export const BASE_URL_FRONT = environment.baseUrlFront;
export const BASE_URL_API = environment.baseUrlApi;

export class HeadTitle {
  public static readonly BACKOFFICE = 'Back Office';
}

export class Advertisement {
  public static readonly ADVERTISEMENT_URI = `${BASE_URL_API}/api/Advertisement`;
  public static readonly ADVERTISEMENT_PAGINATION_URI = `${Advertisement.ADVERTISEMENT_URI}/pagination`;
  public static readonly ADVERTISEMENT_DELETE_URI = `${Advertisement.ADVERTISEMENT_URI}/Delete`;
  public static readonly ADVERTISEMENT_NAMES_URI = `${Advertisement.ADVERTISEMENT_URI}/nomes`;
}


export class Api {
  public static readonly API_URI = `${BASE_URL_API}/api/Api`;
  public static readonly API_BRANDS_URI = `${Api.API_URI}/Brands`;
  public static readonly API_MODELS_URI = `${Api.API_URI}/Model`;
  public static readonly API_VEHICLES_URI = `${Api.API_URI}/Vehicles`;
  public static readonly API_VERSIONS_URI = `${Api.API_URI}/Version`;
}