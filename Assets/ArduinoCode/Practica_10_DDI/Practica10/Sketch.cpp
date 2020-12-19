
#define F_CPU 4000000UL
#include <Arduino.h>
#include <avr/power.h>
#include <util/delay.h>
#include <string.h>
#include <TimerOne.h>

#define ADC0 54
#define ADC1 55
#define PWM 13


char buffer[50];
String msgReceived;
bool receiveComplete = false;

volatile int temp_refresh = 0;
volatile int lum_refresh = 0;

int getLuminisense();
int getTemperature();
void proccesString();
void ISR_updateTime();


void setup() {
  clock_prescale_set (clock_div_8);

  Serial.begin(76800);

  DDRA = 0xFF;
  PORTA = 0x00;
  
  msgReceived.reserve(120);
  
  Timer1.initialize(500000);
  Timer1.attachInterrupt(ISR_updateTime);
  
  DDRB |= 1 << PB7;
  
  Serial.println("Ready");
  
}

void loop() {
    
	if(receiveComplete){
		proccesString();
	}
	
	
	if(lum_refresh > 2){
		
		if(Serial.availableForWrite()){
			sprintf(buffer, "Luminocidad = %d Lx", getLuminisense());
			
			Serial.println(buffer);
		}
		lum_refresh = 0;
	}
	
	if(temp_refresh > 3){
		if(Serial.availableForWrite()){
			sprintf(buffer, "Temperatura = %d Celsius", getTemperature());
			Serial.println(buffer);
		}
		
		temp_refresh = 0;
	}
	
  _delay_ms(10);
}
void ISR_updateTime(){
	temp_refresh++;
	lum_refresh++;
}
int getTemperature()
{
	float lecture = (float)analogRead(ADC1);
	float voltaje = (float)(lecture * 5.f / 1023.f);
	float temperature = (float)(voltaje - 0.40f) / 0.02f;
	
	
	return temperature;
}
void proccesString()
{
	msgReceived.trim();
	
	if(msgReceived.equals("ROOM_LIGHT")){
		PORTA ^= 1 << PA0;
	}
	
	if(msgReceived.equals("LED_RED")){
		PORTA ^= 1 << PA1;
	}
	if(msgReceived.equals("LED_WHITE")){
		PORTA ^= 1 << PA2;
	}
	if(msgReceived.equals("LED_GREEN")){
		PORTA ^= 1 << PA3;
	}

	if(msgReceived.startsWith("MOTOR_SPEED")){
		
		msgReceived.replace("MOTOR_SPEED", "");
		msgReceived.trim();
		
		int dutyCicle = msgReceived.toInt();
		analogWrite(PWM, dutyCicle);
	}
	
	msgReceived = "";
	receiveComplete = false;
	
	
}
int getLuminisense()
{
	int lecture = (analogRead(ADC0));
	float voltaje = (float)(lecture * 5.f / 1023.f); 
	int luminiscense = (voltaje * 15 * 5);
	luminiscense = luminiscense - (luminiscense * 0.10) + 1;
	
	return luminiscense;
	
}
void serialEvent()
{
	if(Serial.available()){
		char c = (char)Serial.read();
		
		if(c == '\n'){
			
			receiveComplete = true;
		}
		else{
			
			msgReceived += c;
		}
		
	}
	
}