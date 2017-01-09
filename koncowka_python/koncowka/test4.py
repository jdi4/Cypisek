import glob
import os
import wx
import urllib2
import random
import datetime
import time
from requests import Session
from signalr import Connection
from wx.lib.pubsub import setuparg1
from wx.lib.pubsub import pub as Publisher
import xml.etree.ElementTree as ET
import threading

#global varaible
G_newHarm = 1 
G_harmString=""
lock = threading.Lock()
class ViewClass(wx.Panel):
    def __init__(self, parent):
         
        wx.Panel.__init__(self, parent)
        self.currentPicture = 0
        self.totalPictures = 0
        width, height = wx.DisplaySize()
        self.photoMaxSize = height - 200
        self.picPaths = []
        self.picTime = []
        self.canPlay=0
       # loadImage()
        Publisher.subscribe(self.PlayImages, ("Play Images"))
        Publisher.subscribe(self.ScheduleChange, ("Schedule change"))

        # layout definition
        self.mainSizer = wx.BoxSizer(wx.VERTICAL)
        img = wx.EmptyImage(self.photoMaxSize,self.photoMaxSize)
        self.imageCtrl = wx.StaticBitmap(self, wx.ID_ANY, 
                                         wx.BitmapFromImage(img))
        
        self.mainSizer.Add(self.imageCtrl, 0, wx.ALL|wx.CENTER, 5)
        
        
        self.slideTimer = wx.Timer(None)
        self.slideTimer.Bind(wx.EVT_TIMER, self.update)
        
    def ScheduleChange(self,msg):
        print "----Schedule change!"
        self.currentPicture = 0
        self.totalPictures = 0
        self.picPaths = []
        self.picTime = []
        if(self.slideTimer.IsRunning()):
            self.slideTimer.Stop()
        #self.PlayImages(msg)
            
    def PlayImages(self,msg):
        print "--PlayImages!"
        #msg to picPath i picTime
        self.picPaths=msg.data[0]
        self.picTime=msg.data[1]
        self.currentPicture = 0
        self.totalPictures =len(self.picPaths)
        self.loadImage(self.picPaths[0])
        self.setScheduleTimer(int(self.picTime[0]))
        
    def loadImage(self, msg):
        
        print "wyswietlam: "
        image=msg
        image_name = os.path.basename(image)
        img = wx.Image(image, wx.BITMAP_TYPE_ANY)
        # scale the image, preserving the aspect ratio
        W = img.GetWidth()
        H = img.GetHeight()
        if W > H:
            NewW = self.photoMaxSize
            NewH = self.photoMaxSize * H / W
        else:
            NewH = self.photoMaxSize
            NewW = self.photoMaxSize * W / H
        img = img.Scale(NewW,NewH)

        self.imageCtrl.SetBitmap(wx.BitmapFromImage(img))
        #self.imageLabel.SetLabel(image_name)
        self.Refresh()
        Publisher.sendMessage("resize", "")
    def NextPic(self):
        if self.currentPicture == self.totalPictures-1:
            self.currentPicture = 0
        else:
            self.currentPicture += 1
        self.loadImage(self.picPaths[self.currentPicture]) 
        
    def setScheduleTimer(self, time):
        """
        Starts and stops the slideshow
        """
        #po skonczeniu odliczania wywoluje metode update
        self.slideTimer.Start(time)
            
    def update(self, event):
        Publisher.sendMessage("check news", "a")
        self.NextPic()
        self.setScheduleTimer(int(self.picTime[self.currentPicture]))
        
        print "Next image in:"+ self.picTime[self.currentPicture]+" milisec"
     

class ViewerFrame(wx.Frame):
     def __init__(self):
        """Constructor"""
        wx.Frame.__init__(self, None, title="Timer test")
        panel = ViewClass(self)
        #self.folderPath = ""
        Publisher.subscribe(self.resizeFrame, ("resize"))
        
        self.sizer = wx.BoxSizer(wx.VERTICAL)
        self.sizer.Add(panel, 1, wx.EXPAND)
        self.SetSizer(self.sizer)
        
        self.Show()
        self.sizer.Fit(self)
        self.Center()
        
     def resizeFrame(self, msg):
        """"""
        self.sizer.Fit(self)
        
class Bunch:
    def __init__(self,**kwds):
        self.__dict__.update(kwds)

class mainCon(threading.Thread):
    def __init__(self):
        print "SignalR Thread init..."
        threading.Thread.__init__(self)
        #globaly obcject to keep schedule informtion
    def run(self):
        
        #self.harmString="Harmonogram1,07/01/2017 16:00,09/01/2017 11:46,4,12.jpg,800,620_PSACD_l-150x150.jpg,800,Copley-Square-21019-150x150.jpg,800,Groups_GroupSupport_Team-300x300.jpg,800"
        
        print "SignalR Thread run..."
        def receiveData(data="NULL"):
            
            print "---HARM change! Receive data: "+str(data)
           # Publisher.sendMessage("Schedule change ", picPaths)
           # getSchedule(harmString):
           # self.harmString=data
        def test1(data="NULL"):
            global G_harmString
            global G_newHarm
            print "----HARM RECEIVE: "+str(data)
            #data="Harmonogram1,07/01/2017 16:00,11/01/2017 11:46,4,12.jpg,8000,620_PSACD_l-150x150.jpg,8000,Copley-Square-21019-150x150.jpg,8000,Groups_GroupSupport_Team-300x300.jpg,8000"
            print "zadam set harm..."
            with lock:
                
                G_harmString=data
                G_newHarm=0
                print "set Gharm to: "+ str(G_harmString)
             
                 
        with Session() as session:
           connection = Connection("http://cypisek.azurewebsites.net/signalr", session)
           chat = connection.register_hub('contentHub') #ContentHub
           #start a connection
           connection.start()    


        #Autentykacja koncowki
        print "Autentykacja jako ID 2"
        chat.client.on('receiveData', receiveData)
        chat.client.on('test1', test1) 
        
        chat.server.invoke('PoorAuthenticate','2')
        time.sleep(1)
        chat.server.invoke('InvokeSending')
        
        with connection:
            connection.wait(1000)    
class main():
    def __init__(self):
        Publisher.subscribe(self.setHarm, ("set harm"))
        Publisher.subscribe(self.checkNews, ("check news"))
        #globaly obcject to keep schedule informtion
        self.harm = Bunch() 
        self.harmString=""

        if(self.internetCheck()):
              
             #self.harmString="Harmonogram1,07/01/2017 16:00,09/01/2017 11:46,4,12.jpg,8000,620_PSACD_l-150x150.jpg,8000,Copley-Square-21019-150x150.jpg,8000,Groups_GroupSupport_Team-300x300.jpg,8000"
             #with connection:
             #  connection.wait(100)
             
             while (1):
                 
                 print "Oczekiwanie na harmonogram..."
                 time.sleep(1)
                 if(self.checkNews()):
                     print "wypadam!"
                     break
                 
             
        else:
             self.readXML()
             #playschedule bez pobierania
             self.playSchedule(1)  
    
    def checkNews(self,msg=""):
        print "Check news!"
        global G_newHarm
        global G_harmString
        if(int(G_newHarm)==0):
           print "receive G_harmString!:"+str(G_harmString)
           self.setHarm(G_harmString)
           with lock:
              G_newHarm=1
           
           return 1   
        else:
            return 0    
    def setHarm(self,msg):
        print "SetHarm: Ustawiam harm..."
        self.harmString=msg
        self.getSchedule(self.harmString)
        self.writeXML()
        self.playSchedule()       
    def writeXML(self):
        a=ET.Element('config')
        b1=ET.SubElement(a,"ID").text=self.harm.ID
        b2=ET.SubElement(a,"ScheduleName",endTime=self.harm.endTime,startTime=self.harm.startTime).text=self.harm.schedule_name
        b4=ET.SubElement(a,"playlist",count=str(len(self.harm.files)))
        #c=ET.SubElement(a,"playlist").text=""
        for index in range(0,len(self.harm.files)):
            ET.SubElement(b4,"file",time=str(self.harm.timers[index])).text=str(self.harm.files[index])
        tree=ET.ElementTree(a)
        tree.write("config.xml") 
        
    def readXML(self):
        tree=ET.parse("config.xml")
        root=tree.getroot()
        count=int(root[2].attrib.get('count'))
        ID=root[0].text;
        scheduleName=root[1].text
        fileList=[]
        timeList=[]
        print "koncowka ID: "+ID
        print "Harmonogram name: "+scheduleName
        print "Files count: "+str(count)
        startTime=root[1].attrib.get("startTime")
        endTime=root[1].attrib.get("endTime")
        for index in range(0,count):
            print "-File:  "+root[2][index].text+", time="+root[2][index].attrib.get("time")
            fileList.append(root[2][index].text);
            timeList.append(root[2][index].attrib.get("time"))
        # kasowanie harmonogramu
        self.harm=Bunch()
        self.harm=Bunch(schedule_name=scheduleName,ID=ID,startTime=startTime,endTime=endTime,files=fileList,timers=timeList) 
    def playSchedule(self,offline=0):
         count=len(self.harm.files)
         picPath=[]
         picTime=[]   
         playStart=self.harm.startTime;
         playEnd=self.harm.endTime;
         
         playStart=time.strptime(playStart,"%d/%m/%Y %H:%M")
         playEnd=time.strptime(playEnd,"%d/%m/%Y %H:%M")
         #1 arg to data 2arg to czas
         #playStart=playStart.split(" ")
         #playEnd=playEnd.split(" ")
         nowTime=datetime.datetime.now()
         nowTime=nowTime.strftime("%d/%m/%Y %H:%M")
         nowTime=time.strptime(nowTime,"%d/%m/%Y %H:%M")
         print(nowTime)
         print(playStart)  
         print(playEnd)   
         for index in range(0,count):
             picPath.append(self.harm.files[index])
             picTime.append(self.harm.timers[index])
             if (offline==0):
                self.getImage(self.harm.files[index])
             
         msg=[picPath, picTime]
         isHarm=1
         while(isHarm):
             nowTime=datetime.datetime.now()
             nowTime=nowTime.strftime("%d/%m/%Y %H:%M")
             nowTime=time.strptime(nowTime,"%d/%m/%Y %H:%M")
             
             if(nowTime>=playStart) and (nowTime<=playEnd):
                isHarm=0
                print "Harmonogram start..."
                Publisher.sendMessage("Schedule change", msg)
                Publisher.sendMessage("Play Images", msg)
             else:
                print "Oczekiwanie na wlaczenie harmonogramu..."  
                
                print "----Aktualny czas: "+str(nowTime.tm_hour)+":"+str(nowTime.tm_min)+" "+str(nowTime.tm_mday)+"/"+str(nowTime.tm_mon)+"/"+str(nowTime.tm_year)
                print "Harmonogram start: "+str(playStart.tm_hour)+":"+str(playStart.tm_min)+" "+str(playStart.tm_mday)+"/"+str(playStart.tm_mon)+"/"+str(playStart.tm_year)
                print ""
                time.sleep(5)  
                #self.waitToStart(msg,playStart,playEnd)            
    
    def getImage(self,file_name):
        pic_path="http://cypisek.azurewebsites.net/mediastorage/"
        
        #pic_path="https://oa.org/files/jpg/"
        url=pic_path+str(file_name)
        print( 'Pobrano zdjecie:', file_name)
       # file_name= file_name+'.jpg'
        with open(file_name,'wb') as f:
            f.write(urllib2.urlopen(url).read())
            f.close()
        
          
        print "zaladowano "+str(file_name)
        
    def setID(self):
        ID='ID'
        for x in range (0,16):
            ID = ID+str(random.randrange(0, 9))
        return ID 
         
    def internetCheck(self):
        try :
            url = "http://cypisek.azurewebsites.net"
            urllib2.urlopen(url);
            print"Connected - ONLINE MODE"
            return 1
        except :
            print "Not connect - OFFLINE MODE"
            return 0         
    def getSchedule(self,harmString):
        lista1=[]
        lista2=[]
        words=harmString.split(",")
        startTime=words[1]
        endTime=words[2]
        w=int(words[3])
        arg=4;
        while (w): 
            lista1.append(words[arg]);
            lista2.append(words[arg+1])
            arg+=2
            w=w-1
        self.harm=Bunch(schedule_name=words[0],startTime=startTime,endTime=endTime,ID=self.setID(),files=lista1,timers=lista2) 
        
glowna= mainCon()
glowna.start() 
      
if __name__ == "__main__":
    
    app = wx.App()
    frame = ViewerFrame()
    main=main() 
    app.MainLoop()
        