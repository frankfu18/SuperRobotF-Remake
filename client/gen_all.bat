set GEN_JAR=../tool/perfect_gen.jar
set CSV_DIR=../cfg
set MSG_DIR=../../IdleXianXiaServer/gsd/msg

set OUT_DIR=Assets/Perfect/Message/Gen
set CSharp_DIR=Assets/Perfect/EditorConfig

java -Xms1g -cp %GEN_JAR% perfect.gen.cfg.Main -xml %CSV_DIR%/cfg_sc.xml -e %CSharp_DIR%/Gen -g client -d Assets/Res/config -l cs -sl 1

java -cp %GEN_JAR% perfect.gen.rpc.Main -x %MSG_DIR%/gs/msg.xml -o %OUT_DIR% -l cs -r client

pause
