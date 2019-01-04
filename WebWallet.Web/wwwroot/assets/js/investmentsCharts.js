$(function () {
    "use strict"
     /*

                        USD/BGN 1.71579/91 ^
                        EUR/USD 1.13982/99 ^
                        GBP/USD 1.26257/66 ^
                        USD/JPY 107.863/73 >

         */

    //var crypto = [];
    //var mapApiResponse = function (res) {
    //    var key = Object.keys(res);
    //    var symbol = key[0].replace('_', '/');
    //    return {
    //        [symbol]: res[key].val
    //    }
    //};

    //var pushUrl = function (src, dest) {
    //    src.forEach(abbr => dest.add(`https://free.currencyconverterapi.com/api/v5/convert?q=${abbr}_BGN&compact=y`));
    //};

    


   // $.getJSON("https://localhost:5001/Authenticated/Report/AllReports", res=> getData(JSON.parse(res)));



    //var checkData = setInterval(function () {
    //    if (data.length === 5) {
    //        clearInterval(checkData);
    //        var dataString = JSON.stringify(data);
    //        $("#userrCurrencyExchangeRates").append(`<div>${dataString}</div>`);
    //    }
    //}, 500);


    var counter = 1;
    var currencies = {
        "USD": "Щатски долар",
        "GBP": "Британска лира",
        "CHF": "Швейцарски франк",
        "CNY": "Китайски ренминби юан",
        "AUD": "Австралийски долар",
        "CAD": "Канадски долар",
        "INR": "Индийска рупия",
        "RUB": "Руска рубла", 
        "TRY": "Нова турска лира",
        "XAU":"Злато (в трой унции)"
    };


    var getJsonData = function (urls) {
        for (var i = 0; i < urls.length; i++) {
            $.getJSON(urls[i], res => onApiResponse(res));
        }
    }

    var onApiResponse = function (src) {
        var key = Object.keys(src);
        var code = key[0].substr(0, 3);
        var val = src[key].val;
        $("#currencies").append(`<tr>
                    <th>${counter}</th>
                    <th>${currencies[code]}</th>
                    <th>${code}</th>
                    <th>${val}</th>
                    <th>${1 / val}</th>
                    </tr>`);
        counter += 1;
    };

    var getUrls = function (data) {
        var urls = [];
        var from = Object.keys(data);
        from.forEach(function (code) {
            urls.push(`https://free.currencyconverterapi.com/api/v5/convert?q=${code}_BGN&compact=y`);
        });

        return urls;
    };

    getJsonData(getUrls(currencies));

    var cryptCompareApiUrl = "https://min-api.cryptocompare.com/data/pricemulti?fsyms=BTC,ETH,EOS,XRP,BCH,XLM,LTC,USDT,XMR,BTG&tsyms=BTC,USD,EUR&api_key=INSERT-4d4d3ac455d9a39f217a07222c698ac2edf0c227b721ca1bee091421961624a7";
    var crypto = {
        "BTC": "Bitcoin",     
        "ETH": "Ethereum",    
        "EOS": "EOS",    
        "XRP": "Ripple",      
        "BCH": "Bitcoin Cash",
        "XLM": "Stellar",     
        "LTC": "Litecoin",    
        "USDT": "Tether",      
        "XMR": "Monero",      
        "BTG":"Bitcoin Gold"
    };

    $.getJSON(cryptCompareApiUrl, function (res) {
        var counter = 1;
        var codes = Object.keys(res);
        codes.forEach(function (code) {
            var rates = res[code];
            $("#crypto").append(`<tr>
                    <th>${counter}</th>
                    <th>${crypto[code]}</th>
                    <th>${code}</th>
                    <th>${rates.BTC}</th>
                    <th>${rates.USD}</th>
                    <th>${rates.EUR}</th>
                    </tr>`);

            counter += 1;
        });
    });
});