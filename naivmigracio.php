<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        //
        Schema::create('zene', function (Blueprint $table) {


    $table->engine='InnoDB';
    
        $table->bigIncrements('id');
            $table->unsignedBigInteger('zeneid')->primary()->autoIncrement();
            $table->text('zeneurl')->unique();//->nullable();
            $table->text('eloado')->nullable();
            $table->text('cim')->nullable();//defaultnak a url utolso / utani resze, de dinamikusan talan after funkcioval  default();//slug
            //$table->slug=
            $table->boolean('lejatszhatoe')->default(false);
            $table->unsignedBigInteger('hossz')->nullable();
            $table->text('tema')->nullable();
            //torolve bool oszlop?
            $table->timestamps(6);
            
            });
            Schema::create('felhasznalo', function (Blueprint $table) {
            
             $table->engine='InnoDB';
            $table->unsignedBigInteger('felhasznaloid')->primary()->autoIncrement();
            $table->text('jelszoh')->comment('hashben');//->nullable();
            $table->text('nev')->unique();
            $table->text('email')->nullable();
            $table->boolean('letiltott')->default(false);
            $table->enum('jog',["demo","normalis","admin","tanar","asztali"]);//->nullable();
            //torolve bool oszlop
            $table->timestamps(6);
            });
             Schema::create('keres', function (Blueprint $table) {

              $table->engine='InnoDB';
                //zene bekeresnel ide kerul adat is ....
            $table->unsignedBigInteger('keresid')->primary()->autoIncrement();
             //keresid?=rajid
             $table->foreignId('felhasznaloid')->constrained('felhasznalo')->onDelete('');
            $table->foreignId('zeneid')->constrained('zene')->onDelete('');
            $table->timestamps();
            
            
            });

 Schema::create('esemeny', function (Blueprint $table) {
// pl felhasznalo inserted vagy zene updated
              $table->engine='InnoDB';
              $table->unsignedBigInteger('esemenyid')->primary()->autoIncrement();
           $table->enum('esemeny',['insert','update','delete']);
           $table->text('hol');
           $table->text('regirow')->nullable();
           $table->text('ujrow')->nullable();
           $table->timestamps();
           

              });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        //
        Schema::dropIfExists('zene');
    }
};
