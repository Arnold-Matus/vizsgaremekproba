<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class keresmodel extends Model
{
    //
       /*
      $table->id();
             $table->foreignId('felhasznaloid')->constrained('felhasznalo')->onDelete('');
            $table->foreignId('zeneid')->constrained('zene')->onDelete('');
            $table->boolean('validalte')->default(false);
          //  $table->timestamps();
          $table->timestamp("mikor");
        */
       public $table = "keres";
    //public $fillable = ["felhasznaloid","zeneid","validalte","mikor"];
    public $fillable = ["felhasznaloid","zeneurl","validalte","mikor"];

    public $timestamps=false;
    public function felhasznalo(){
    //return $this->belongsTo(felhasznalomodel::class,"felhasznaloid","id");
   return $this->belongsToMany(felhasznalomodel::class,"felhasznaloid","id");
    }
/*    public function zene(){
    //return $this->belongsTo(zenemodel::class,"zeneid","id");
   return $this->belongsToMany(zenemodel::class,"zeneid","id");
    }*/
}
