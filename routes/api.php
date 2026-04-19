<?php

use App\Http\Controllers\orarendkontroler;
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
Route::post('/bejelentkezes',[felhasznalokontroller::class,'bejelentkezes']);
Route::get('/lejatszandozene',[orarendkontroler::class,'jelenlegizene']);
Route::get('/kilepes',[felhasznalokontroller::class,'kilepes']);
Route::get('/kileptetesemailalapjan/{email}',[felhasznalokontroller::class,'kileptetesemailalapjan']);
Route::delete('/felhasznalotorlese/{email}',[felhasznalokontroller::class,'felhasznalotorlesemailalapjan']);
Route::delete('/felhasznalotorlese',[felhasznalokontroller::class,'jelenlegifelhasznalotorlese']);
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
