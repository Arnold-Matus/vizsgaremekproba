<?php

namespace App\Http\Controllers;

use App\Models\zenemodel;
use Illuminate\Http\Request;

class zenekontroler extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function lekerosszes()
    {
        //
        
    }
    public function lekerosszeszene(){
        if (request()->ajax()) {
            //return view("");
            return response()->json(zenemodel::all(),200, ["Content-type"=> "application/json"]);
        }
        else{
            return response()->json("ajaxxal kapcsolatos problema",403, ["Content-type"=> "application/json"]);
        }
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
    public function show(zenemodel $zenemodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, zenemodel $zenemodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(zenemodel $zenemodel)
    {
        //
    }
}
