import { Component, OnInit, Input, SimpleChanges, SimpleChange } from '@angular/core';
import { MarkerInfo } from '../Models/MarkerInfo';
import { GeoLocation } from '../Models/Geolocation';
import { Polyline } from '../Models/Polyline';
import { StationService } from '../station.service';
import { Observable } from 'rxjs';
import { UpdateStationBindingModel } from '../Models/AddStationBindingModel';
import { LineModel } from '../Models/LineModel';
import { DrivelineService } from '../driveline.service';
import * as mapTypes from '@agm/core/services/google-maps-types';
import { } from 'googlemaps';
import { MapsAPILoader } from '@agm/core';

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] //postavljamo sirinu i visinu mape
})
export class MapComponent implements OnInit{

  @Input() selectedLineNumber : number;
 
  markerInfos : any[] = [];
  public polyline: Polyline;
  public zoom: number;
  stations = new Observable<UpdateStationBindingModel>();
  selectedStations = new Observable<UpdateStationBindingModel>();
  lines : LineModel[] = [];
  stationNumber : number;
  selectedStationCount : number;
  options : any;
  busline : LineModel;
  line : any;
  route : any[] = [];
  //directionService = new google.maps.DirectionsService;


  constructor(private stationService : StationService , private drivelineService : DrivelineService, private directionsService : google.maps.DirectionsService) { }

  ngOnInit() {

    this.options = {  
      suppressMarkers : true    //brisanje defaulth markera
    };

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

      });

      
    });

  
  }

  ngOnChanges(changes: SimpleChanges) {
    if (this.selectedLineNumber != undefined)
      this.getStationsForSelectedLine();
    
  } 

  getStationsForSelectedLine()
  {
    this.selectedStationCount = 0;
    this.drivelineService.GetStationsByNumber(this.selectedLineNumber).subscribe(stations=>
      {
        this.selectedStations = stations;
        this.selectedStations.forEach(station=>
          {
            this.selectedStationCount++;
          });

          this.placeLines();
      });
  }

  placeLines()
  {
    // this.lines = [];
    // for(var i = 0; i < this.selectedStationCount - 1; i++)
    // {
    //     let lineModel = new LineModel();
    //     lineModel.origin = { lat: this.selectedStations[i].X, lng: this.selectedStations[i].Y};
    //     lineModel.destination = {lat: this.selectedStations[i + 1].X , lng: this.selectedStations[i+1].Y}
    //     this.lines.push(lineModel);     
    // }
    //     let lineModel = new LineModel();
    //     lineModel.origin = { lat: this.selectedStations[this.selectedStationCount-1].X, lng: this.selectedStations[this.selectedStationCount-1].Y};
    //     lineModel.destination = {lat: this.selectedStations[0].X , lng: this.selectedStations[0].Y}
    //     this.lines.push(lineModel)

        this.busline = new LineModel();
        this.busline.origin = { lat: this.selectedStations[0].X, lng: this.selectedStations[0].Y };
        this.busline.destination = { lat: this.selectedStations[0].X, lng: this.selectedStations[0].Y };
        this.busline.waypoints = [];
        let index = 0;
        this.selectedStations.forEach(station => {
          if (index)
          {
            this.busline.waypoints.push( {location: {lat: station.X, lng: station.Y}, stopover: false} );
          }
          index++;
        });

        this.getRoute(this.busline.origin, this.busline.destination, this.busline.waypoints);
        //console.log(this.testPanel);
  }

  getRoute(origin, destination, waypoints) {
    let request = {
      origin: origin,
      destination: destination,
      waypoints: waypoints,
      travelMode: google.maps.TravelMode.DRIVING
    };
    
    this.directionsService.route(request, function(result, status) {
      if (status == google.maps.DirectionsStatus.OK) {
        this.route = [];
        result.routes.forEach(element => {
          let location = new GeoLocation(element.legs[0].steps[0].start_location.lat(), element.legs[0].steps[0].start_location.lng());
          this.route.push(location);
          element.legs[0].steps.forEach(step => {
            let location = new GeoLocation(step.end_location.lat(), step.end_location.lng());
            this.route.push(location);
          }); 
        });
      }

      this.route.forEach(rt => {
        console.log(rt);
      });
    });
  }

  // placeMarker($event){
  //   this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
  //   console.log(this.polyline)
  // }

}
