<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class orarendmodel extends Model
{
    //
    /* $table->id();      
   $table->foreignId('zeneid')->constrained('zene')->onDelete('');
   $table->timestamp('mikortol')->default(now());
$table->timestamp('meddig')->default(now());
   //$table->enum("lejatszva");
     */
     public $table = "orarend";
    public $fillable = ["zeneid","mikortol","meddig"];//,"ujsor","CREATED_AT ","UPDATED_AT"];
    public $timestamps=false;
    //ez igazabol egy queu amit majd egy event minden nap ujrafeltolt adatokkal

    public function orarendconverter($hanyadikszunet,){//$milyennap=now()->format("Y-m-d H:i:s")){
         $szunetek=["8-30","9-25","10-20","11-20","12-15","13-10","14-20","15-10","16-00"];//10,10,15,10,10,25,5
         return $szunetek[$hanyadikszunet];
    }
    public function zene(){
    return $this->belongsToMany(zenemodel::class,"zeneid");
    }

}
