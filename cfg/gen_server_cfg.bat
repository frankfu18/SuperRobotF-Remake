set GEN_JAR=../tool/perfect_gen.jar
set CSV_DIR=..\cfg

..\tool\JDK10\bin\java.exe -cp %GEN_JAR% perfect.gen.cfg.Main -xml %CSV_DIR%\cfg_sc.xml -c gen\cfg -g server -l java -d .\config

pause
