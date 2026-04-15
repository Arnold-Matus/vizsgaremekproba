<?php

namespace App\Http\Controllers;

use App\Models\felhasznalomodel;
use Illuminate\Http\Request;

class felhasznalokontroller extends Controller
{
    /**
     * Display a listing of the resource.
     */
     /**$table->engine='InnoDB';
          //  $table->unsignedBigInteger('felhasznaloid')->primary()->autoIncrement();
          $table->id(); 
          $table->text('jelszoh')->comment('hashben');//->nullable();
            $table->text('nev')->unique();
            $table->text('email')->nullable();
            $table->boolean('letiltott')->default(false);
            $table->enum('jog',["demo","normalis","admin","tanar","asztali"]);//->nullable();
            //torolve bool oszlop
            $table->string("omazonosito",11)->nullable();
            $table->timestamps(6); */
    public function index()
    {
        //
    }




    public function regisztracio(Request $request){

    function jogkezeles($jogg,Request $request){
    if($jogg=="admin" && $request->user()->jog=="admin"){

    }
    }
       // $_COOKIE[""] = $request->session()->get("");
   // $keresztnve=$request->keresztnev;
   // $vezeteknev=$request->vezeteknev;
    $validalas= $request->validate([]);
    if($validalas->fails()){
       // return redirect()->back()->withErrors($validalas->errors());
   return response("nem validalt",404);
       }
       else{
        felhasznalomodel::create(['jelszoh'=> md5($request->jelszo),'keresztnev'=>$request->keresztnev,'vezeteknev'=>$request->vezeteknev,'email'=>$request->email,'jog'=>$request->jog,'omazonosito'=>$request->omazonosito,'created_at'=>now()]);//\Auth::user()->id,''=>$validalas->id]);
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
    public function show(felhasznalomodel $felhasznalomodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, felhasznalomodel $felhasznalomodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(felhasznalomodel $felhasznalomodel)
    {
        //
    }
}
