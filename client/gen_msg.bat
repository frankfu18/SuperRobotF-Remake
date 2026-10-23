set GEN_JAR=../tool/perfect_gen.jar
set MSG_DIR=../../IdleXianXiaServer/gsd/msg

set OUT_DIR= Assets/Perfect/Message/Gen

java -cp %GEN_JAR% perfect.gen.rpc.Main -x %MSG_DIR%/gs/msg.xml -o %OUT_DIR% -l cs -r client


pause
