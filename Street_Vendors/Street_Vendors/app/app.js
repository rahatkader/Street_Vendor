var hub = $.connection.mapHub;

var firstTime = 1;
var locStatus;
var countOwn = 0;
var markerClientIndex = [];
var markerClients = {};
var markerId = 0;
var markers = {};
var clients = {};


hub.client.message = function (loc) {
    
    var newMarker;
    var newClientMarker;
    var latlong = loc.split(',');

    var who = latlong[0];
    var latitude = parseFloat(latlong[1]).toFixed(6);
    var longitude = parseFloat(latlong[2]).toFixed(6);
    var newCoords = new google.maps.LatLng(latitude, longitude);
    var status = latlong[3];
    var clientId = latlong[4];
    if (firstTime) {
        $("#location").append("<li>" + newCoords + "</li>");
    }
    
    if (who == "own") {
        
        const image = "../images/userMark.png";
        if (countOwn == 0) {
            countOwn = 1;
            newMarker = new google.maps.Marker({
                position: newCoords,
                icon: image,
                map: map
            });
            map.setCenter(newCoords);
            map.setZoom(12);

            markers[0] = newMarker;
            var contentString = '<div id="content">' +
                '<div id="siteNotice">' +
                "</div>" +
                '<h1 id="firstHeading" class="firstHeading">' + "Your Location" + '</h1>' +
                '<div id="bodyContent">' +
                '<p><b>' + "Your Realtime location using SignalR and Javascript Map API." + '</b>';

            // marker location info window
            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });
            // user hover
            newMarker.addListener('mouseover', function () {
                infowindow.open(map, newMarker);
            });

            // user unhover
            newMarker.addListener('mouseout', function () {
                infowindow.close();
            });

        } else {
            newMarker = markers[0];
            //newMarker.setMap(null);
            newMarker.setPosition(newCoords);
        }
        
    } else {
        
        if (markerClientIndex.includes(who)) {
            if (status == 0) {
                newCoords = new google.maps.LatLng("","");
            }
            var temp = markerClientIndex.indexOf(who);
            console.log(temp);
            newClientMarker = markerClients[temp];
            //newMarker.setMap(null);
            newClientMarker.setPosition(newCoords);
        } else {
            if (status == 1) {
                newClientMarker = new google.maps.Marker({
                    position: newCoords,
                    map: map
                });
                console.log(markerId);
                markerClients[markerId] = newClientMarker;
                markerClientIndex[markerId] = who;
                clients[markerId] = clientId;
                markerId++;

                var contentString = '<div id="content">' +
                    '<div id="siteNotice">' +
                    "</div>" +
                    '<h1 id="firstHeading" class="firstHeading">' + who + '</h1>' +
                    '<div id="bodyContent">' +
                    '<p><b>' + who + '</b>';

                // marker location info window
                var infowindow = new google.maps.InfoWindow({
                    content: contentString
                });

                // other hover
                newClientMarker.addListener('mouseover', function () {
                    infowindow.open(map, newClientMarker);
                });
                
                // other unhover
                newClientMarker.addListener('mouseout', function () {
                    infowindow.close();
                });
                
                //other shop link on-Click
                newClientMarker.addListener("click", () => {
                    ////refernce here to that users shop
                    var tempId = markerClientIndex.indexOf(who);
                    userClientId = clients[tempId];
                    window.location.href = "/MarketPlace/ProfileDetails/" + userClientId;
                    
                });
            }
            
        }
        
    }
 
}


hub.client.user = function (msg) {
    $("#user").append("<li>" + msg + "</li>");
}


$.connection.hub.start(function () {

    ///////////////////// FOR DEMO REALTIME FUNCTIONALITY OF THIS MAP /////////////////////
    /*
    $("#send").click(function () {
        if ($("#locSwitch")[0].checked) {
            locStatus = 1;
        } else {
            locStatus = 0;
        }

        var str = $("#txt").val();
        str = str + "," + locStatus;
        console.log(str);

        hub.server.send(str);
        $("#txt").val(" ");
    })
    */
    
    
    ///////////////////// FOR ACTUAL REALTIME FUNCTIONALITY OF THIS MAP /////////////////////

    const interval = setInterval(sLocation, 5000);
    function sLocation() {

        if ($("#locSwitch")[0].checked) {
            locStatus = 1;
        } else {
            locStatus = 0;
        }

        var clientX;
        var clientY;
        navigator.geolocation.getCurrentPosition(success, failure);

        function success(position) {
            clientX = position.coords.latitude;
            clientY = position.coords.longitude;
            console.log(clientX, clientY);

            var clientCoords = clientX + "," + clientY + "," + locStatus;
            hub.server.send(clientCoords);
        }
        function failure() {
            if (firstTime) {
                firstTime = 0;
                console.log("Your browser does not support.")
                var clientmsg = 'Your browser does not support.';
                $("#location").append("<li>" + clientmsg + "</li>");
                clientmsg = 'Go to "Settings" > "Advanced" > "Privacy and security" > "Site settings" > "Location" => Turn ON';
                $("#location").append("<li>" + clientmsg + "</li>");
                hub.server.send("");
            } 
        }
    }
    
})