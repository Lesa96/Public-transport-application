import { GeoLocation } from "./Geolocation";

export class MarkerInfo {
    iconUrl: string;
    title: string;
    label: string;
    location: GeoLocation;

    constructor(location: GeoLocation, icon: string, title:string, label:string){
        this.iconUrl = icon;
        this.title = title;
        this.label = label;
        this.location = location;
    }
} 