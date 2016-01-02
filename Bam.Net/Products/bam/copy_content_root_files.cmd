RMDIR /S /Q %1\Bam.Net\Products\bam\app
RMDIR /S /Q %1\Bam.Net\Products\bam\content
MD %1\Bam.Net\Products\bam\app
MD %1\Bam.Net\Products\bam\content
del /F /Q %1\Bam.Net\BamContent\apps\localhost\localhost.js
del /F /Q %1\Bam.Net\BamContent\apps\localhost\localhost.min.js
xcopy %1\Bam.Net\BamContent\apps\localhost %1\Bam.Net\Products\bam\app\ /E
xcopy %1\Bam.Net\BamContent\apps\localhost %1\Bam.Net\Products\bam\content\apps\localhost\ /E
copy %1\Bam.Net\BamContent\apps\include.js %1\Bam.Net\Products\bam\content\apps\include.js
RMDIR /S /Q %1\Bam.Net\BamContent\common\dao
RMDIR /S /Q %1\Bam.Net\BamContent\common\workspace
MD %1\Bam.Net\BamContent\common\dao
MD %1\Bam.Net\BamContent\common\workspace
xcopy %1\Bam.Net\BamContent\common %1\Bam.Net\Products\bam\content\common\ /E