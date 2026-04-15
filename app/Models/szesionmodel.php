<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class szesionmodel extends Model
{
    //
      /*$table->text('session')->primary();
            $table->timestamps();
            $table->foreignId('felhasznaloid')->constrained('felhasznalo')->onDelete('');
            //controllerbe ha updatedat legalabb 30 perce volt NOW() hpz kepest akkor nem ervenyes, de lehet frontend mar ezt elintezte
*/
 public $table = "esemeny";
    public $fillable = ["felhasznaloid","session","CREATED_AT ","UPDATED_AT"];
    public $timestamps=false;

public function felhasznalo(){
    return $this->belongsToMany(felhasznalomodel::class,"felhasznaloid","id");
    }

}
