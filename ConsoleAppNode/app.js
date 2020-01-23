'use strict';

var rp = require('request-promise');
var readline = require('readline-sync');
const request = require('request');

var querystring = require('querystring');
var http = require('http');

var rawData = {
    "identifier": "MKS",
    "orderType": 0,
    "orderSize": 123,
    "orderSubType": 0,
    "instruction": "fill",
    "orderTransmissionType": 1
};

var options = {
    method: 'POST',
    uri: 'https://localhost:44344/api/trade/',
    body: rawData,
    json: true // Automatically stringifies the body to JSON
};

rp(options)
    .then(function (parsedBody) {
        console.log(parsedBody);
        // POST succeeded...
    })
    .catch(function (err) {
        console.log(parsedBody);
        // POST failed...
    });

request.post('https://localhost:44344/api/trade', {
    json: {
        todo: 'Buy the milk'
    }
}, (error, res, body) => {
    if (error) {
        console.error(error)
        return
    }
    console.log(`statusCode: ${res.statusCode}`)
    console.log(body)
});



//request({
//    url: https://localhost:44344/api/trade,
//    method: "POST",
//    headers: {
//        "content-type": "application/json"
//    },
//    json: jsonObject
//},
//    function (error, resp, body) {
//        console.write("hello ducky2");
//    };
//});



PostCode("hello");



console.log('Hello world');
readline.question("Press any key to continue");


function PostCode(codestring) {
    // Build the post string from an object
    var post_data = querystring.stringify({
        'compilation_level': 'ADVANCED_OPTIMIZATIONS',
        'output_format': 'json',
        'output_info': 'compiled_code',
        'warning_level': 'QUIET',
        'js_code': codestring
    });

    // An object of options to indicate where to post to
    var post_options = {
        host: ' https://localhost:44344/api/trade',
        port: '44344',
        path: '/compile',
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Content-Length': Buffer.byteLength(post_data)
        }
    };

    // Set up the request
    var post_req = http.request(post_options, function (res) {
        res.setEncoding('utf8');
        res.on('data', function (chunk) {
            console.log('Response: ' + chunk);
        });
    });

    // post the data
    post_req.write(post_data);
    post_req.end();

}