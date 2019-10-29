$(document).ready(function () {

function mapLocation() {
  var directionsDisplay;
  var directionsService = new google.maps.DirectionsService();
  var map;

  function initialize() {
      directionsDisplay = new google.maps.DirectionsRenderer();

      var chicago = new google.maps.LatLng(23.777176, 90.399452);
    var mapOptions = {
      zoom: 7,
      center: chicago
    };
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    directionsDisplay.setMap(map);
    calcRoute();
   // google.maps.event.addDomListener(document.getElementById('routebtn'), 'click', calcRoute);
  }

  function calcRoute() {
      var start = new google.maps.LatLng($("#userlat").val(), $("#userlon").val());
 
      var end = new google.maps.LatLng($("#reslat").val(), $("#reslon").val());
    var request = {
      origin: start,
      destination: end,
      travelMode: google.maps.TravelMode.DRIVING
    };
    directionsService.route(request, function(response, status) {
      if (status == google.maps.DirectionsStatus.OK) {
          directionsDisplay.setDirections(response);        
          directionsDisplay.setMap(map);

      } 
    });
  }

  google.maps.event.addDomListener(window, 'load', initialize);
}
mapLocation();




//location between distence

var p1 = new google.maps.LatLng($("#userlat").val(), $("#userlon").val());
var p2 = new google.maps.LatLng($("#reslat").val(), $("#reslon").val());

$("#distenceFromUser").html(calcDistance(p1, p2)+" KM");


//calculates distance between two points in km
function calcDistance(p1, p2) {
    return (google.maps.geometry.spherical.computeDistanceBetween(p1, p2) / 1000).toFixed(2);
}



});