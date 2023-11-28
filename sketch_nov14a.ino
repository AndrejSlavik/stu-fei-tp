#include <ArduinoBLE.h>
#include <Arduino_LSM6DS3.h>

BLEService gyroService("19B10000-E8F2-537E-4F6C-D104768A1214");
BLECharacteristic gyroCharacteristic("19B10001-E8F2-537E-4F6C-D104768A1214", BLERead | BLENotify, 6);

void setup() {
  Serial.begin(9600);
  // set built-in LED pin to output mode
  pinMode(LED_BUILTIN, OUTPUT);

  digitalWrite(LED_BUILTIN, HIGH);         // will turn the LED on
  delay(100);
  digitalWrite(LED_BUILTIN, LOW);          // will turn the LED off
  delay(100);
  digitalWrite(LED_BUILTIN, HIGH);         // will turn the LED on
  delay(100);
  digitalWrite(LED_BUILTIN, LOW);          // will turn the LED off
  delay(100);
  digitalWrite(LED_BUILTIN, HIGH);         // will turn the LED on
  delay(100);
  digitalWrite(LED_BUILTIN, LOW);          // will turn the LED off
  delay(100);
  digitalWrite(LED_BUILTIN, HIGH);         // will turn the LED on
  delay(100);
  digitalWrite(LED_BUILTIN, LOW);          // will turn the LED off

  if (!BLE.begin()) {
    Serial.println("Starting BLE failed!");
    while (1);
  }

  if (!IMU.begin()) {
    Serial.println("Failed to initialize IMU!");
    while (1);
  }

  BLE.setLocalName("Nano33IoT");
  BLE.setAdvertisedService(gyroService);

  gyroService.addCharacteristic(gyroCharacteristic);
  BLE.addService(gyroService);

  BLE.advertise();
  Serial.println("Bluetooth device active, waiting for connections...");
}

void loop() {
  digitalWrite(LED_BUILTIN, HIGH);         // will turn the LED on
  delay(1000);
  digitalWrite(LED_BUILTIN, LOW);          // will turn the LED off
  delay(1000);
  digitalWrite(LED_BUILTIN, HIGH);         // will turn the LED on
  delay(1000);
  digitalWrite(LED_BUILTIN, LOW);          // will turn the LED off

  // listen for BLE peripherals to connect:
  BLEDevice central = BLE.central();

  // if a central is connected to peripheral:
  if (central) {
    digitalWrite(LED_BUILTIN, LOW);
    Serial.print("Connected to central: ");
    // print the central's MAC address:
    Serial.println(central.address());

    // while the central is still connected to peripheral:
    while (central.connected()) {
      digitalWrite(LED_BUILTIN, HIGH);

      float x, y, z;

      if (IMU.gyroscopeAvailable()) {
        IMU.readGyroscope(x, y, z);

        Serial.print(x); Serial.print(',');
        Serial.print(y); Serial.print(',');
        Serial.println(z);
      }

      // Send a keep-alive message (assuming 1 byte)
      uint8_t keepAlive = 0; // Change the value if needed
      gyroCharacteristic.writeValue(keepAlive);

      delay(5000); // Add a 5-second delay before checking the connection again
    }

    // when the central disconnects, print it out:
    Serial.print(F("Disconnected from central: "));
    Serial.println(central.address());
    digitalWrite(LED_BUILTIN, LOW); // will turn the LED off

    // Add a delay before advertising again
    delay(1000);
  }
}
