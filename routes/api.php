<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;
use App\Models\felhasznalomodel;

Route::get('/user', function (Request $request) {
    return Response($request->user(),200); //$request->user();
})->middleware('auth:sanctum');
//osszes zene get, oraarend get, felhasznalo ad torol modosit login logout, ...//zene modosit ha kell asztali
//mi lenne ha nem sanctumot hasznalnank hanem csak a user model vagy controllerbe lenne egy useradatoktokenbol ami a where(token,parameter) response
Route::get('/teszttt', function () {return response("oke",204);});
Route::get('/felhasznalotokenbol/{token}', function (Request $r) {return response()->json(felhasznalomodel::where('token', $r->header("toke"))->first(),200,['Content-Type'=>'application/json']);});
Route::get('/ido', function () {return response(Carbon\Carbon::now() ,200);});

