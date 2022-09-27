import * as internal from "assert";

export class PostcodeLocation {
  distanceMiles: string;
  distanceKm: string;
  district: string;
  longitude: number;
  latitude: string;

  constructor(distanceMiles, distanceKm, district, longitude, latitude) {
    this.distanceMiles = distanceMiles;
    this.distanceKm = distanceKm;
    this.district = district;
    this.longitude = longitude;
    this.latitude = latitude;
  }
}
