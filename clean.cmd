@ECHO OFF
PUSHD %~dp0

IF EXIST "bin" (
    RMDIR /s /q "bin"
)

POPD