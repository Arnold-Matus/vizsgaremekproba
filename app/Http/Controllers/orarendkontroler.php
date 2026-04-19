<?php

namespace App\Http\Controllers;

use App\Models\orarendmodel;
use Carbon\Carbon;
use Illuminate\Http\Request;

class orarendkontroler extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
    }
    public function jelenlegizene(Request $r){
    //nincs validacio, token publikus
    $talanmostani=orarendmodel::where("meddig",">=",Carbon::now())->orderBy("mikor")->limit(1)->first();

    if(empty($talanmostani)){return response("nincs",404);}
    if($talanmostani->mikor>=Carbon::now()){return response($talanmostani->zeneurl,"200");}
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(orarendmodel $orarendmodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, orarendmodel $orarendmodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(orarendmodel $orarendmodel)
    {
        //
    }
}
