<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;
use Illuminate\Support\Facades\DB;
return new class extends Migration
{
    /**
     * Run the migrations.
     */
    /*
    public function up(): void
    {
        //
        if (!Schema::hasTable('zene')) {
        Schema::create('zene', function (Blueprint $table) {


    $table->engine='InnoDB';
    
       // $table->bigIncrements('id');
        //    $table->unsignedBigInteger('zeneid')->primary()->autoIncrement();
       $table->id();
       $table->text('zeneurl')->unique();//->nullable();
   //  $table->foreign('zeneurl')->references('zeneurl')->on('keres');//->onDelete('');
            $table->text('eloado')->nullable();
            $table->text('cim')->nullable();//defaultnak a url utolso / utani resze, de dinamikusan talan after funkcioval  default();//slug
            //$table->slug=
            $table->boolean('lejatszhatoe')->default(true);
            $table->unsignedBigInteger('hossz')->nullable();
            $table->text('tema')->nullable();
            //torolve bool oszlop?
            $table->timestamps(6);
            

       $table->foreign('keresid')->references('id')->on("keres");


            });
        }
        if (!Schema::hasTable('felhasznalo')) {
            Schema::create('felhasznalo', function (Blueprint $table) {
            
             $table->engine='InnoDB';
          //  $table->unsignedBigInteger('felhasznaloid')->primary()->autoIncrement();
          $table->id(); 
          $table->text('jelszoh')->comment('hashben');//->nullable();
            $table->text('keresztnev');//->unique();
            $table->text('vezeteknev');//->unique();
            $table->text('email')->nullable()->unique();
            $table->boolean('letiltott')->default(false);
            $table->enum('jog',["demo","normalis","admin","tanar","asztali"]);//->nullable();
            //torolve bool oszlop
            $table->string("omazonosito",11)->nullable()->unique();
            $table->timestamps(6);
            });
            //credential userhez lehetne kulon tabla foreignid
        }
        if (!Schema::hasTable('keres')) {
             Schema::create('keres', function (Blueprint $table) {

              $table->engine='InnoDB';
                //zene bekeresnel ide kerul adat is ....
           // $table->unsignedBigInteger('keresid')->primary()->autoIncrement();
             //keresid?=rajid
             $table->id();
             $table->foreignId('felhasznaloid')->constrained('felhasznalo');//->onDelete('');
           // $table->foreignId('zeneid')->constrained('zene');//->onDelete('');
           $table->text('zeneurl')->unique();
         //  $table->text('zenecim')->nullable();
          // $table->text('z')
            $table->boolean('validalte')->default(false);
           // $table->timestamps();
           $table->timestamp('mikor')->default(now());
            
            
            });
        }
        if (!Schema::hasTable('esemeny')) {

 Schema::create('esemeny', function (Blueprint $table) {
// pl felhasznalo inserted vagy zene updated
              $table->engine='InnoDB';
            //  $table->unsignedBigInteger('esemenyid')->primary()->autoIncrement();
          $table->id();
            $table->enum('sqlesemeny',['insert','update','delete']);
           $table->text('tabla')->nullable();
          // $table->text('melyiksorok')->nullable();
           $table->text('regisor')->nullable();
           $table->text('ujsor')->nullable();
           $table->timestamps();
           

          // $table->foreignId('ki')->constrained('');//->onDelete('');
    $table->text('ki')->nullable();
  //  $table->timestamps();
              });
        }
          
        if (!Schema::hasTable('orarend')) {
    Schema::create('orarend', function (Blueprint $table) {
    $table->engine= 'InnoDB';
   // $table->unsignedBigInteger('esemenyid')->primary()->autoIncrement();
   $table->id();      
   $table->foreignId('zeneid')->constrained('zene');//->onDelete('');
   $table->timestamp('mikortol')->default(now());
$table->timestamp('meddig')->default(now());
   //$table->enum("lejatszva");
        });
        }

        if (!Schema::hasTable('szesion')) {

        Schema::create('szesion', function (Blueprint $table) {
            $table->engine= 'InnoDB';
            $table->string('session',32)->primary();
            $table->timestamps();
            $table->foreignId('felhasznaloid')->constrained('felhasznalo');//->onDelete('');
         /*   //controllerbe ha updatedat legalabb 30 perce volt NOW() hpz kepest akkor nem ervenyes, de lehet frontend mar ezt elintezte DB::unprepared('CREATE event IF NOT EXISTS orarendfeltoltes ON SCHEDULE EVERY 1 DAY DO  ');
       // DB::unprepared('create view asztalialkalmazasnakview');
       //viewok is triggerek:
      // TIMESTA
      *//*
   

        }
    
        
        );
        }

        //orarenddel mi legyen??
     Db::unprepared('CREATE VIEW IF NOT EXISTS feltoltendok as select id,zeneurl from zene where zeneurl LIKE "http%"');
     
//        DB::unprepared(' CREATE TRIGGER IF NOT EXISTS zenebekeresfeltoltesorarendbe AFTER INSERT on keres FOR EACH ROW    IF ( (NEW.validalte=1 | NEW.validalte=TRUE) && ((SELECT lejatszhatoe from zene where id like NEW.zeneid LIMIT 1 ) IN (TRUE,1))) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES (NEW.zeneid,select meddig from orarend ORDER BY mikortol DESC LIMIT 1,
 //       select TIMESTAMPADD(SECOND,select hossz from zene where id like NEW.zeneid, SELECT meddig from orarend ORDER BY mikortol  DESC LIMIT 1));END IF;');//ADDTIME((SELECT mikortol from orarend ORDER BY mikortol  DESC LIMIT 1),(select hossz from zene where id LIKE (SELECT zeneid from orarend ORDER BY mikortol  DESC LIMIT 1)))
 DB::unprepared('CREATE TRIGGER IF NOT EXISTS zenebekeresfeltoltesorarendbe AFTER INSERT on keres FOR EACH ROW BEGIN IF ( NEW.validalte && (SELECT lejatszhatoe from zene where id = NEW.zeneid LIMIT 1 )) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES (NEW.zeneid,(select meddig from orarend ORDER BY mikortol DESC LIMIT 1), (select TIMESTAMPADD(SECOND, (select hossz from zene where id like NEW.zeneid), (SELECT meddig from orarend ORDER BY mikortol DESC LIMIT 1) ) ) ) ; END IF; END;');
 //orarendbe beszurasnal nem nezi hogy szuneten kivulre esik-e event ami megadja hogy meddig lehet jatszani uj tablaba
 
 //(select ADDTIME( hossz,hossz) from zene where id LIKE (SELECT zeneid from orarend ORDER BY mikortol  DESC LIMIT 1)
     //  DB::unprepared('create view '); zenebekeresvalidaciofrissitesorarendbehelyezes
        DB::unprepared(' CREATE TRIGGER IF NOT EXISTS zenebekeresvalidaciofrissitesorarendbehelyezes AFTER UPDATE on keres FOR EACH ROW    IF ( (NEW.validalte=1 | NEW.validalte=TRUE) && ((SELECT lejatszhatoe from zene where id like NEW.zeneid LIMIT 1 ) IN (TRUE,1))) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES (NEW.zeneid,select meddig from orarend ORDER BY mikortol DESC LIMIT 1,
        select TIMESTAMPADD(SECOND,select hossz from zene where id like NEW.zeneid, SELECT meddig from orarend ORDER BY mikortol  DESC LIMIT 1));END IF;');
    }*/
         public function up(): void
    {
        //$table->charset = 'utf8mb4';
    //$table->collation = 'utf8mb4_bin';
        //
          if (!Schema::hasTable("jogok")) {
           
          Schema::create("jogok", function (Blueprint $table) {
            // $table->charset = 'utf8_hungarian_ci';
  //  $table->collation = ' utf8_hungarian_ci';
           
  $table->engine= "InnoDB";
  $table->unsignedTinyInteger("szint")->primary()->index();
  $table->text("jog")->unique();


          });
        }
        if (!Schema::hasTable('esemeny')) {

 Schema::create('esemeny', function (Blueprint $table) {
// pl felhasznalo inserted vagy zene updated
//$table->charset = 'utf8_hungarian_ci';
  //  $table->collation = ' utf8_hungarian_ci';
              $table->engine='InnoDB';
            //  $table->unsignedBigInteger('esemenyid')->primary()->autoIncrement();
          $table->id();
            $table->enum('sqlesemeny',['insert','update','delete']);
           $table->text('tabla')->nullable();
          // $table->text('melyiksorok')->nullable();
           $table->text('regisor')->nullable();
           $table->text('ujsor')->nullable();
           $table->timestamps();
           

          // $table->foreignId('ki')->constrained('');//->onDelete('');
    $table->text('ki')->nullable();
  //  $table->timestamps();
              });
        }
          if (!Schema::hasTable('szunetek')) {
          Schema::create('szunetek', function (Blueprint $table) {
           // $table->charset = 'utf8_hungarian_ci';
   // $table->collation = ' utf8_hungarian_ci';
        $table->engine='InnoDB';
        $table->integer("hanyadik")->primary()->index();
        $table->timestamp("kezdes")->useCurrent();
        $table->timestamp("vege")->useCurrent();


          });
        }
        if (!Schema::hasTable('felhasznalo')) {
            Schema::create('felhasznalo', function (Blueprint $table) {
         //   $table->charset = 'utf8_hungarian_ci';
   // $table->collation = ' utf8_hungarian_ci';
             $table->engine='InnoDB';
          //  $table->unsignedBigInteger('felhasznaloid')->primary()->autoIncrement();
          $table->id(); 
          $table->text('jelszoh')->comment('hashben');//->nullable();
            $table->text('keresztnev');//->unique();
            $table->text('vezeteknev');//->unique();
            $table->text('email')->nullable()->unique();
            $table->boolean('letiltott')->default(false);
           // $table->enum('jog',["demo","normalis","admin","tanar","asztali"]);//->nullable();
          $table->unsignedTinyInteger('jog');
         // bcrypt()
        // Hash::make('')->nullable();
            //torolve bool oszlop
            $table->string("omazonosito",11)->nullable()->unique();
            $table->timestamps(6);
            $table->boolean("emailverifikalva")->default(false);
            $table->string("token")->nullable()->index();
           
            $table->timestamp("tokenvaliditasanakvege")->nullable();
             $table->foreign('jog')->references('szint')->on('jogok');
            });
            //credential userhez lehetne kulon tabla foreignid
        }
        if (!Schema::hasTable('keres')) {
             Schema::create('keres', function (Blueprint $table) {
//$table->charset = 'utf8_hungarian_ci';
   // $table->collation = ' utf8_hungarian_ci';
              $table->engine='InnoDB';
                //zene bekeresnel ide kerul adat is ....
           // $table->unsignedBigInteger('keresid')->primary()->autoIncrement();
             //keresid?=rajid
             $table->id();
             $table->foreignId('felhasznaloid')->references('id')->on('felhasznalo');//->constrained('felhasznalo');//->onDelete('');
           // $table->foreignId('zeneid')->constrained('zene');//->onDelete('');
           $table->text('zeneurl')->index();//->unique();
         //  $table->text('zenecim')->nullable();
          // $table->text('z')
            $table->boolean('validalte')->default(false);
           // $table->timestamps();
           $table->timestamp('mikor')->default(now());
            
            
            });
        }
        if (!Schema::hasTable('zene')) {
        Schema::create('zene', function (Blueprint $table) {

//$table->charset = 'utf8_hungarian_ci';
  //  $table->collation = ' utf8_hungarian_ci';
    $table->engine='InnoDB';
    
       // $table->bigIncrements('id');
        //    $table->unsignedBigInteger('zeneid')->primary()->autoIncrement();
       $table->id();
       $table->text('zeneurl')->unique()->nullable()->index();//->nullable(); ///NULLABOL LEHET TOBB EZ MOST JO DE ERDEKES
   //  $table->foreign('zeneurl')->references('zeneurl')->on('keres');//->onDelete('');
            $table->text('eloado')->nullable();
            $table->text('cim')->nullable();//defaultnak a url utolso / utani resze, de dinamikusan talan after funkcioval  default();//slug
            //$table->slug=
            $table->boolean('lejatszhatoe')->default(true);
            $table->unsignedBigInteger('hossz')->nullable();
            $table->text('tema')->nullable();
            //torolve bool oszlop?
            $table->timestamps(6);
            
       $table->text("keresurl")->unique()->nullable();
       //$table->foreign('keresid')->references('id')->on("keres"); //egy zenehez nem csak egy keres tartozhat de egy kereshez egy zene de keresbe nem lehet zeneid mert meg akkor nincs zenerekord


            });
        }
        
          
        if (!Schema::hasTable('orarend')) {
    Schema::create('orarend', function (Blueprint $table) {
     // $table->charset = 'utf8_hungarian_ci';
  //  $table->collation = ' utf8_hungarian_ci';
    $table->engine= 'InnoDB';
   // $table->unsignedBigInteger('esemenyid')->primary()->autoIncrement();
   $table->id();      
   $table->foreignId('zeneid')->references('id')->on('zene');//->constrained('zene');//->onDelete('');
   $table->timestamp('mikortol')->default(now())->index();
$table->timestamp('meddig')->default(now());
   //$table->enum("lejatszva");
        });
        }
      
      
  
       // if (!Schema::hasTable('szesion')) {
       if(false){

        Schema::create('szesion', function (Blueprint $table) {
            $table->engine= 'InnoDB';
            $table->string('session',32)->primary();
            $table->timestamps();
            $table->foreignId('felhasznaloid')->constrained('felhasznalo');//->onDelete('');
         /*   //controllerbe ha updatedat legalabb 30 perce volt NOW() hpz kepest akkor nem ervenyes, de lehet frontend mar ezt elintezte DB::unprepared('CREATE event IF NOT EXISTS orarendfeltoltes ON SCHEDULE EVERY 1 DAY DO  ');
       // DB::unprepared('create view asztalialkalmazasnakview');
       //viewok is triggerek:
      // TIMESTA
      */
   

        }
    
        
        );
        }

        //orarenddel mi legyen??
     Db::unprepared('CREATE VIEW IF NOT EXISTS feltoltendok as select id,zeneurl from zene where zeneurl LIKE "http%"');
     
//        DB::unprepared(' CREATE TRIGGER IF NOT EXISTS zenebekeresfeltoltesorarendbe AFTER INSERT on keres FOR EACH ROW    IF ( (NEW.validalte=1 | NEW.validalte=TRUE) && ((SELECT lejatszhatoe from zene where id like NEW.zeneid LIMIT 1 ) IN (TRUE,1))) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES (NEW.zeneid,select meddig from orarend ORDER BY mikortol DESC LIMIT 1,
 //       select TIMESTAMPADD(SECOND,select hossz from zene where id like NEW.zeneid, SELECT meddig from orarend ORDER BY mikortol  DESC LIMIT 1));END IF;');//ADDTIME((SELECT mikortol from orarend ORDER BY mikortol  DESC LIMIT 1),(select hossz from zene where id LIKE (SELECT zeneid from orarend ORDER BY mikortol  DESC LIMIT 1)))
 //DB::unprepared('CREATE TRIGGER IF NOT EXISTS zenebekeresfeltoltesorarendbe AFTER INSERT on keres FOR EACH ROW BEGIN IF ( NEW.validalte && (SELECT lejatszhatoe from zene where zeneurl = NEW.zeneid LIMIT 1 )) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES (NEW.zeneid,(select meddig from orarend ORDER BY mikortol DESC LIMIT 1), (select TIMESTAMPADD(SECOND, (select hossz from zene where id like NEW.zeneid), (SELECT meddig from orarend ORDER BY mikortol DESC LIMIT 1) ) ) ) ; END IF; END;');
DB::unprepared(' CREATE TRIGGER IF NOT EXISTS zenebekeresfeltoltesorarendbe AFTER INSERT on keres FOR EACH ROW BEGIN IF ( NEW.validalte && (SELECT lejatszhatoe from zene where zene.keresurl = NEW.zeneurl LIMIT 1 )) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES ((SELECT zene.id from zene where zene.keresurl = NEW.zeneurl LIMIT 1 ),(select meddig from orarend ORDER BY mikortol DESC LIMIT 1), (select TIMESTAMPADD(SECOND, (select hossz from zene where zene.keresurl like NEW.zeneurl), (SELECT meddig from orarend ORDER BY mikortol DESC LIMIT 1) )) ) ; END IF; END;;');
 //orarendbe beszurasnal nem nezi hogy szuneten kivulre esik-e event ami megadja hogy meddig lehet jatszani uj tablaba
 
 //(select ADDTIME( hossz,hossz) from zene where id LIKE (SELECT zeneid from orarend ORDER BY mikortol  DESC LIMIT 1)
     //  DB::unprepared('create view '); zenebekeresvalidaciofrissitesorarendbehelyezes
     //   DB::unprepared(' CREATE TRIGGER IF NOT EXISTS zenebekeresvalidaciofrissitesorarendbehelyezes AFTER UPDATE on keres FOR EACH ROW    IF ( (NEW.validalte=1 | NEW.validalte=TRUE) && ((SELECT lejatszhatoe from zene where id like NEW.zeneid LIMIT 1 ) IN (TRUE,1))) THEN INSERT INTO orarend (zeneid,mikortol,meddig) VALUES (NEW.zeneid,select meddig from orarend ORDER BY mikortol DESC LIMIT 1,
    //    select TIMESTAMPADD(SECOND,(select hossz from zene where id like NEW.zeneid), (SELECT meddig from orarend ORDER BY mikortol  DESC LIMIT 1)));END IF;');
      DB::unprepared('CREATE VIEW IF NOT EXISTS aktivfelhasznalok as SELECT count(id) from felhasznalo where token is not null');
    DB::unprepared('CREATE VIEW IF NOT EXISTS Lejatszhatozenek as select * from zene where zene.zeneurl is not null');
    DB::unprepared('SET SESSION time_zone ="+2:00"');
    }
    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        //
       Schema::dropIfExists('szunetek');
    
        Schema::dropIfExists('jogok');
        Schema::dropIfExists('esemeny');
        Schema::dropIfExists('orarend');
        Schema::dropIfExists('szesion');
        Schema::dropIfExists('zene');
        Schema::dropIfExists('keres');
        Schema::dropIfExists('felhasznalo');
        Schema::dropIfExists('szunetek');

    }
};
