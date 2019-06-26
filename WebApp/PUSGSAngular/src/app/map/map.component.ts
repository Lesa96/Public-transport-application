import { Component, OnInit, Input, SimpleChanges, SimpleChange, NgZone } from '@angular/core';
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
import { RoutesBindingModel } from '../Models/RoutesBindingModel';
import { NotificationService } from '../notification.service';


@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}'] //postavljamo sirinu i visinu mape
})
export class MapComponent implements OnInit{

  @Input() selectedLineNumber : number;
  @Input() bussGeolocation : GeoLocation;
 
  markerInfos : any[] = [];
  bussMarkers : any[] = [];
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
  sendRoutes = new RoutesBindingModel();
  isConnected: Boolean;
  notifications: string[];
  locationFromBack: string;
  showBusses = false;


  constructor(private notifService: NotificationService,private ngZone: NgZone, private stationService : StationService , private drivelineService : DrivelineService, private directionsService : google.maps.DirectionsService) { }

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

    //--------------------------------------------------------------
    
    this.checkConnection();
    this.subscribeForNotifications();
    this.subscribeForTime();
    this.notifService.registerForClickEvents();
    

  
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
        this.route= [];
        result.routes.forEach(element => {
          let location = new GeoLocation(element.legs[0].steps[0].start_location.lat(), element.legs[0].steps[0].start_location.lng());
          this.route.push(location);
          element.legs[0].steps.forEach(step => {
            let location = new GeoLocation(step.end_location.lat(), step.end_location.lng());
            this.route.push(location);
            
          }); 
        }
        );

      }
      
     // this.sendRoutes = new RoutesBindingModel();
     console.warn(this.selectedLineNumber);
      //this.sendRoutes.LineNumber = this.selectedLineNumber;
      this.route.forEach(rt => {
        console.log(rt); //tacke skretanja verovatno
        
       // this.sendRoutes.RouteCoordinates.push(rt);
      });

      
    }.bind(this)   
    );
  }

  sendRoutesToBack()
  { 
    

    console.warn(this.notifService.ConnectionID);

    if(this.route.length != 0)
    {
      this.sendRoutes.RouteCoordinates = [];
      this.route.forEach(rt =>
        {
          this.sendRoutes.RouteCoordinates.push(rt);
        //  console.warn(rt);
        })
      this.sendRoutes.LineNumber = this.selectedLineNumber;
      this.sendRoutes.ConId = this.notifService.ConnectionID;
      console.warn("CON ID JE:  "+this.sendRoutes.ConId);
      this.drivelineService.SendLineNumberAndRoutes(this.sendRoutes).subscribe();
    }   
    
    this.showBusses = true;
  }

  // placeMarker($event){
  //   this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng))
  //   console.log(this.polyline)
  // }

  //---------------------------------------------------------------------------------------
  private checkConnection(){
    this.notifService.startConnection().subscribe(e => {this.isConnected = e; 
        if (e) {
          this.notifService.StartTimer()
        }
    });
  }

  private subscribeForNotifications () {
    this.notifService.notificationReceived.subscribe(e => this.onNotification(e));
  }

  public onNotification(notif: string) {

    this.ngZone.run(() => { 
      this.notifications.push(notif);  
      console.log(this.notifications);
   });  
 }

 subscribeForTime() {
  this.notifService.registerForTimerEvents().subscribe(e => this.onTimeEvent(e));
}

public onTimeEvent(location: string){
  this.ngZone.run(() => { 
     this.locationFromBack = location; 
  });  
  console.warn(this.locationFromBack); //ono sto smo dobili sa back-a

  if(this.locationFromBack != "")
  {
    this.bussMarkers = [];
    let coors = this.locationFromBack.split(';');
    coors.forEach(location =>
      {
        let coor = location.split(',');
        if(coor[0] != "" && coor[1] != "")
        {
          let lat = +coor[0]; // + pretvara u broj, ako ne uspe vrati 0
          let lng = +coor[1];
          
  
          let newMarker = new MarkerInfo(new GeoLocation(lat,lng),
                "assets/busicon.png","Buss.Name" ,"Line.Number"
                )
          this.bussMarkers.push(newMarker);
        }
        
      })
    
  }
  

}

public startTimer() {
  this.notifService.StartTimer();
}

public stopTimer() {
  this.notifService.StopTimer();
  this.locationFromBack = "";
}

}
