
import os

import time

import threading
from requests import Session
from signalr import Connection

class watek(threading.Thread):
    def __init__(self,threadName):
        self.threadName=threadName
        print threadName+"init..."
        threading.Thread.__init__(self)
        #globaly obcject to keep schedule informtion
        
    def run(self):
        def receiveData(data="NULL"):
            
            print "---HARM change! Receive data: "+str(data)
           # Publisher.sendMessage("Schedule change ", picPaths)
           # getSchedule(harmString):
           # self.harmString=data
        def test1(data="NULL"):
            print "---------------Jordan mowi "+str(data) 
            #self.harmString=data        
        with Session() as session:
           connection = Connection("http://cypisek.azurewebsites.net/signalr", session)
           chat = connection.register_hub('contentHub') #ContentHub
           #start a connection
           connection.start()    


        #Autentykacja koncowki
        print "Autentykacja jako ID 2"
        chat.client.on('receiveData', receiveData)
        chat.client.on('test1', test1) 
        
        chat.server.invoke('PoorAuthenticate','3')
        time.sleep(2)
        chat.server.invoke('InvokeSending')
            
        with connection:
              connection.wait(10000)
              

def main():
    print "Glowny watek init.."
    watek1= watek("Gacek")
    watek1.start()
    
        
            
if __name__ == "__main__":
    main()
    