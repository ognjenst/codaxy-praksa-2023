FROM ubuntu:latest

RUN apt update && apt install iputils-ping -y && apt install nmap -y && apt install  openssh-server -y

RUN useradd -rm -d /home/ubuntu -s /bin/bash -g root -G sudo -u 1000 soc-user 

RUN  echo 'soc-user:soc-user' | chpasswd

RUN service ssh start

EXPOSE 22

CMD ["/usr/sbin/sshd","-D"]