import { Component, OnInit } from '@angular/core';
import { MarkerInfo } from '../Models/MarkerInfo';
import { GeoLocation } from '../Models/Geolocation';
import { Polyline } from '../Models/Polyline';
import { StationService } from '../station.service';
import { Observable } from 'rxjs';
import { UpdateStationBindingModel } from '../Models/AddStationBindingModel';
import { AgmMarker } from '@agm/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] //postavljamo sirinu i visinu mape
})
export class MapComponent implements OnInit {

  // markerInfos = new Observable<MarkerInfo>();
  markerInfos : any[] = [];
  public polyline: Polyline;
  public zoom: number;
  stations = new Observable<UpdateStationBindingModel>();
  
  constructor(private stationService : StationService) { }

  ngOnInit() {

    this.stationService.GetAllStations().subscribe(ss =>{ 
      this.stations = ss;
      var i = 0;
      this.stations.forEach(station=>{
        
        let newMarker = new MarkerInfo(new GeoLocation(station.X,station.Y),
        "assets/BusStationLogo.png",station.Name ,station.Address, ""
        )
        this.markerInfos.push(newMarker);
      });
      

    });

      this.polyline = new Polyline([], 'blue', { url:"assets/busicon.png", scaledSize: {width: 50, height: 50}});
  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    console.log(this.polyline)
  }

}
