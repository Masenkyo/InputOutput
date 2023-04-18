int button = 13;
int buttonsped = 8;
int led = 12;
int deathled = 11;
int swap = 7;
int ledbrightness = 50;
int fade = 5;
String readString = "";
bool death;
bool jumping;
bool speeding;
bool swapping;
void setup() {
  pinMode (button, INPUT_PULLUP);
  pinMode (buttonsped, INPUT_PULLUP);
  pinMode (swap, INPUT_PULLUP);
  pinMode (led, OUTPUT);
  pinMode (deathled, OUTPUT);
  Serial.begin(115200);
}

void loop() {
  if (death == true) {
    analogWrite(deathled, ledbrightness);
    ledbrightness = ledbrightness + fade;
    if (ledbrightness == 100 || ledbrightness == 0){
      fade = -fade;
    }
    delay(50);
  }
  if (!digitalRead(swap) && swapping == true){
    Serial.println("swap");
    swapping = false;
  }
  if (digitalRead(swap) && swapping == false){
    Serial.println("swap not");
    swapping = true;
  }
  if (digitalRead(button) && jumping == true){
    Serial.println("jump not");
    jumping = false;
  }
  if (!digitalRead(button) && jumping == false){
    Serial.println("jump"); 
    jumping = true;
  }
  if (digitalRead(buttonsped) && speeding == true){
    Serial.println("not speed");
    speeding = false;
  }
  if (!digitalRead(buttonsped) && speeding == false){
    Serial.println("speed"); 
    speeding = true;
  }
  while (Serial.available()) {
    delay(3);
    char c = Serial.read();
    readString += c;
  }
  if (readString.length() >0) {
    Serial.println(readString);

    if(readString.indexOf("True") >=0)
    {
      digitalWrite(led, 1);
    }

    if(readString.indexOf("False") >=0)
    {
      digitalWrite(led, 0);
    }

    if (readString.indexOf("Dead!") >=0){
      death = true;
    }
    if (readString.indexOf("Alive!") >=0){
      death = false;
      analogWrite(deathled, 0);
    }

    readString="";
  }
}   