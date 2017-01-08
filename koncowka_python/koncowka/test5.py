
import os

import time

import threading


class watek(threading.Thread):
    def __init__(self,threadName):
        self.threadName=threadName
        print threadName+"init..."
        threading.Thread.__init__(self)
        #globaly obcject to keep schedule informtion
        
    def run(self):
        print self.threadName+"run..."
        
        i=0
        while(i<1000):
            print self.threadName+" mowi ze to "+str(i)+"\n"
            i=i+1
            time.sleep(2)

def main():
    print "Glowny watek init.."
    watek1= watek("Gacek")
    watek2= watek("Wacek")
    watek1.start()
    watek2.start()
    i=0
    while(i<1000):
       print "\nmatka mowi ze j to "+str(i)
       i=i+1
       time.sleep(1)
        
            
if __name__ == "__main__":
    main()
    