<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class felhasznalomodel extends Model
{
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
    public $table = "felhasznalo";
    public $fillable = ["jelszoh","vezeteknev","keresztnev","email","letiltott","jog","omazonosito","CREATED_AT ","UPDATED_AT"];
    public $timestamps=true;
    public function keres(){
    return $this->hasMany(keresmodel::class,"felhasznaloid","id");
    }
    public function szesion(){
    return $this->hasMany(szesionmodel::class,"felhasznaloid","id");
    }
}
