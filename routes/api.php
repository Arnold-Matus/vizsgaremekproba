<?php

use App\Http\Controllers\kereskontroller;
use App\Http\Controllers\orarendkontroler;
use App\Http\Controllers\szunetkontroller;
use App\Http\Controllers\zenekontroler;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Models\felhasznalomodel;
use App\Http\Controllers\felhasznalokontroller;
Route::get('/user', function (Request $request) {
    return Response($request->user(),200); //$request->user();
})->middleware('auth:sanctum');
//osszes zene get, oraarend get, felhasznalo ad torol modosit login logout, ...//zene modosit ha kell asztali
//mi lenne ha nem sanctumot hasznalnank hanem csak a user model vagy controllerbe lenne egy useradatoktokenbol ami a where(token,parameter) response
Route::get('/teszttt', function () {return response("oke",204);});
Route::get('/felhasznalotokenbol/{token}', function (Request $r) {return response()->json(felhasznalomodel::where('token', $r->header("toke"))->first(),200,['Content-Type'=>'application/json']);});
Route::get('/ido', function () {return response(Carbon\Carbon::now() ,200);});
Route::post('/regisztracio',[felhasznalokontroller::class,'regisztracio']);
Route::post('/nemnormalfelhasznaloregisztracio',[felhasznalokontroller::class,'regisztraciobarmilyenjogut']);
Route::post('/bejelentkezes',[felhasznalokontroller::class,'bejelentkezes']);
Route::get('/lejatszandozene',[orarendkontroler::class,'jelenlegizene']);
Route::get('/lejatszandozenemindenadat',[orarendkontroler::class,'jelenlegizeneminden']);
Route::get('/kilepes',[felhasznalokontroller::class,'kilepes']);
Route::get('/kileptetesemailalapjan/{email}',[felhasznalokontroller::class,'kileptetesemailalapjan']);
Route::delete('/felhasznalotorlese/{email}',[felhasznalokontroller::class,'felhasznalotorlesemailalapjan']);
Route::delete('/felhasznalotorlese',[felhasznalokontroller::class,'jelenlegifelhasznalotorlese']);
Route::get('/aktivfelhasznaloszam',[felhasznalokontroller::class,'aktivfelhasznaloszam']);
Route::get('/osszeszene',[zenekontroler::class,'lekerosszeszene']);
Route::delete('/zenetorlesidalapjan/{id}',[zenekontroler::class,'zenetorlesidalapjan']);
Route::put('/zenefrissites',[zenekontroler::class,'zenefrissites']);
Route::put('/zeneutvonalfrissitesurlalpjan/{url}',[zenekontroler::class,'zeneutvonalfrissitesurlalpjan']);
Route::put('/zeneutvonalfrissitesidalapjan/{id}',[zenekontroler::class,'zeneutvonalfrissitesidalapjan']);
Route::delete('/zenetorles',[zenekontroler::class,'zenetorles']);
Route::delete('/lejatszastorles',[orarendkontroler::class,'lejatszastorles']);
Route::get('/mainapiorarend',[orarendkontroler::class,'mainapiorarend']);
Route::get('/teljesorarend',[orarendkontroler::class,'teljesorarend']);
Route::get('/kovetkezolejatszas',[orarendkontroler::class,'kovetkezolejatszas']);
Route::post('/zenevalidacio/{id}',[kereskontroller::class,'zenevalidacio']);
Route::get('/varolista',[zenekontroler::class,'feltoltendok']);
Route::get('/lejatszhatozenek',[zenekontroler::class,'lejatszhatozenek']);
Route::post('/orarendmanualishozzaadas',[orarendkontroler::class,'lejatszasmanualishozzaadasa']);
Route::post('/zenefeltoltes',[zenekontroler::class,'zenefeltoltes']);

Route::get('/ezenanaponlevoorarend/{mikkorr}', [orarendkontroler::class,'xnapiorarend']);

Route::get('/szuneteklistaja',[szunetkontroller::class,'szuneteklistaja']);
Route::delete('/szunettorles/{hanyadik}',[szunetkontroller::class,'szunettorles']);
Route::post('/szunethozzaadas',[szunetkontroller::class,'szunethozzaadas']);
Route::put('/szunetmodositas/{hanyadik}',[szunetkontroller::class,'szunetmodositas']);
Route::post('/bekeres/{url}',[kereskontroller::class,'bekeres']);
Route::delete('/kerestorles',[kereskontroller::class,'kerestorles']);
Route::get('/kereseklistazasa',[kereskontroller::class,'kereseklistazasa']);
Route::put('/bejelentkezettfelhasznalofrissit',[felhasznalokontroller::class,'bejelentkezettfelhasznalofrissit']);
Route::get('/osszesfelhasznalokilistazasa',[felhasznalokontroller::class,'osszesfelhasznalokilistazasa']);
Route::get('/jelenlegbejelentkezettfelhasznalok',[felhasznalokontroller::class,'jelenlegbejelentkezettfelhasznalok']);
Route::put('/szerepkorkezeles/{email}',[felhasznalokontroller::class,'szerepkorkezeles']);
Route::put('/felhasznaloadatokfrissit/{email}',[felhasznalokontroller::class,'felhasznaloadatokfrissit']);

/* Source - https://stackoverflow.com/a/77859972
// Posted by Rashid 
// Retrieved 2026-04-19, License - CC BY-SA 4.0

// Define routes with the specific prefix
Route::prefix('tps')
->group(function () {
    Route::get('/xxx', function () {
        // ...
    })->middleware('throttle:10,1'); // 10 requests per minute for routes under /tps/xxx
    // Other routes within the prefix
});
 */
