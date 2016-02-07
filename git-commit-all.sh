#!/bin/bash

cd /c/BamContent/apps
/c/src/Bam.Net/git-add.sh
/c/src/Bam.Net/git-commit.sh $1
cd /c/BamContent/common/bamvvm
/c/src/Bam.Net/git-add.sh
/c/src/Bam.Net/git-commit.sh $1
cd /c/src/Business
/c/src/Bam.Net/git-add.sh
/c/src/Bam.Net/git-commit.sh $1
cd /c/BamContent
/c/src/Bam.Net/git-add.sh
/c/src/Bam.Net/git-commit.sh $1
cd /c/src/Bam.Net/BamContent
/c/src/Bam.Net/git-pull.sh --all
cd /c/src/Bam.Net
/c/src/Bam.Net/git-add.sh
/c/src/Bam.Net/git-commit.sh $1
