call %1\Bam.Net\Products\bam\copy_content_root_files.cmd %1
del %1\Bam.Net\Bam.Net.Server\app.base
del %1\Bam.Net\Bam.Net.Server\content.root
%1\Bam.Net\Bam.Net.Server\7zip\7za a -tzip %1\Bam.Net\Bam.Net.Server\app.base %1\Bam.Net\Products\bam\app\*
%1\Bam.Net\Bam.Net.Server\7zip\7za a -tzip %1\Bam.Net\Bam.Net.Server\content.root %1\Bam.Net\Products\bam\content\*
copy %1\Bam.Net\Bam.Net.Server\content.root %1\Bam.Net\BamContent\content.root