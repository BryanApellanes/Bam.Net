rem %1 - commit message
c:
cd \BamContent\apps
call c:\src\Bam.Net\git-add.cmd
call c:\src\Bam.Net\git-commit.cmd %1
cd \BamContent\common\bamvvm
call c:\src\Bam.Net\git-add.cmd
call c:\src\Bam.Net\git-commit.cmd %1
cd \src\Business
call c:\src\Bam.Net\git-add.cmd
call c:\src\Bam.Net\git-commit.cmd %1
cd \BamContent
call c:\src\Bam.Net\git-add.cmd
call c:\src\Bam.Net\git-commit.cmd %1
cd \src\Bam.Net\BamContent
call c:\src\Bam.Net\git-pull.cmd
cd \src\Bam.Net
call c:\src\Bam.Net\git-add.cmd
call c:\src\Bam.Net\git-commit.cmd %1
