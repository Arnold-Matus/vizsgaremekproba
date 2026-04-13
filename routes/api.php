<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('/user', function (Request $request) {
    return $request->user();
})->middleware('auth:sanctum');
//osszes zene get, oraarend get, felhasznalo ad torol modosit login logout, ...//zene modosit ha kell asztali