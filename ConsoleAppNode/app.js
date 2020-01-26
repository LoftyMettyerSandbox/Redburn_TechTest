const https = require('https')
const http = require('http')
var concat = require('concat-stream');
var request = require('request');
var watch = require('node-watch');

const readline = require("readline");
const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

watch('./', { recursive: false }, function (evt, name) {
    console.log('%s changed.', name);
});


//function promptForData() {
    //rl.question("What identifer would you like to retrive ? ", function (identifier) {
    //    console.log(`Retreiving "${identifier}", ...`);

    //    var options = {
    //        hostname: 'localhost',
    //        port: 44344,
    //        path: `api/trade/${identifier}`,
    //        method: 'GET',
    //        rejectUnauthorized: false
    //    }

    //    var req = https.request(options, res => {
    //        console.log(`statusCode: ${res.statusCode}`)

    //        res.on('data', d => {
    //            process.stdout.write(d)
    //        })
    //    })

    //    req.on('error', error => {
    //        console.error(error)
    //    })

    //    //req.end(function () {
    //    //    promptForData();
    //    //}

    //    req.end();
    //});
//}

rl.question("Press any key to exist... ", function (identifier) {
}

rl.on("close", function () {
    console.log("\nBYE BYE !!!");
    process.exit(0);
    });



//    promptForData();

