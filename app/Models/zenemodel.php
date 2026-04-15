<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class zenemodel extends Model
{
    //
    /*      $table->id();
        $table->text('zeneurl')->unique();//->nullable();
            $table->text('eloado')->nullable();
            $table->text('cim')->nullable();//defaultnak a url utolso / utani resze, de dinamikusan talan after funkcioval  default();//slug
            //$table->slug=
            $table->boolean('lejatszhatoe')->default(false);
            $table->unsignedBigInteger('hossz')->nullable();
            $table->text('tema')->nullable();
            //torolve bool oszlop?
            $table->timestamps(6); */
     public $table = "zene";
    public $fillable = ["zeneurl","cim","lejatszhatoe","hossz","tema","CREATED_AT ","UPDATED_AT"];
    public $timestamps=true;
  /*  public function keres(){
    return $this->belongsTo(keresmodel::class,"zeneid","id");
    }*/
    public function orarend(){
    return $this->hasMany(orarendmodel::class,"zeneid","id");
    }
}
