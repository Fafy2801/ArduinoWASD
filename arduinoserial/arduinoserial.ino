int pins[] = { 3, 4, 5, 6 }; // WASD

void setup() {
  Serial.begin(9600);
  // Initialize pins
  for (byte i = 0; i < sizeof(pins) / sizeof(pins[0]); i++) {
    pinMode(pins[i], INPUT_PULLUP);
  }
}

void loop() {
  // Check for pin reading and write to serial
  for (byte i = 0; i < sizeof(pins) / sizeof(pins[0]); i++) {
    long reading = digitalRead(pins[i]);

    if (reading == HIGH) {
      Serial.write("0");
    }
    else {
      Serial.write("1");
    }
  }

  // Finish sending serial for this loop
  Serial.println();
  delay(100);
}
