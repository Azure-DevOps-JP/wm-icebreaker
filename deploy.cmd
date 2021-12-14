@if "%SCM_TRACE_LEVEL%" NEQ "4" @echo off

IF "%SITE_ROLE%" == "function" (
 
  deploy.function.cmd
 
) ELSE (
  deploy.icebreaker.cmd
)