<?php

namespace App\Http\Controllers;

use App\Models\keresmodel;
use Illuminate\Http\Request;

class kereskontroller extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function zenevalidacio(Request $request,$id){
        $a =keresmodel::where("id",$id)->first();
        if(!$a) {
            return response()->json(["nincs ilyen keres",404,["Content-type"=>"application/json"]]);
        }
        $a->update(["validalte"=>true]);
        return response()->json(["validalva",200,["Content-Type"=> "application/json"]]);
    }
    public function index()
    {
        //
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
    public function show(keresmodel $keresmodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, keresmodel $keresmodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(keresmodel $keresmodel)
    {
        //
    }
}
