import { Component, OnInit } from '@angular/core';
import { MarkerInfo } from '../Models/MarkerInfo';
import { GeoLocation } from '../Models/Geolocation';
import { Polyline } from '../Models/Polyline';
import { StationService } from '../station.service';
import { Observable } from 'rxjs';
import { UpdateStationBindingModel } from '../Models/AddStationBindingModel';
import { AgmMarker } from '@agm/core';
import { LineModel } from '../Models/LineModel';

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
  lines : LineModel[] = [];
  stationNumber : number;

  public origin: any;
  public destination: any;
  
  constructor(private stationService : StationService) { }

  ngOnInit() {

    this.stationService.GetAllStations().subscribe(ss =>{ 
      this.stations = ss;
      this.polyline = new Polyline([], 'blue', { url:"", scaledSize: {width: 50, height: 50}}); //linije

      this.stationNumber = 0;
      this.stations.forEach(station=>{
        
        this.stationNumber++; //broj stanica 

        let newMarker = new MarkerInfo(new GeoLocation(station.X,station.Y),
        "assets/BusStationLogo.png",station.Name ,station.Address
        )
        this.markerInfos.push(newMarker);
        //this.polyline.addLocation(new GeoLocation(station.X,station.Y));

      });

      this.placeLines();
      
    });

  
  }

  placeLines()
  {
    for(var i = 0; i < this.stationNumber - 1; i++)
    {
        let lineModel = new LineModel();
        lineModel.origin = { lat: this.stations[i].X, lng: this.stations[i].Y};
        lineModel.destination = {lat: this.stations[i + 1].X , lng: this.stations[i+1].Y}
        this.lines.push(lineModel);     
    }
        let lineModel = new LineModel();
        lineModel.origin = { lat: this.stations[this.stationNumber-1].X, lng: this.stations[this.stationNumber-1].Y};
        lineModel.destination = {lat: this.stations[0].X , lng: this.stations[0].Y}

  }

  placeMarker($event){
    this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
    console.log(this.polyline)
  }

}
