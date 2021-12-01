import java.io.BufferedReader;
import java.io.*;
class payrollExample
{
 public static void main ( String[] args ) {
    
    // for(double X = 3.0;X<6;X++) { double xVal = (3*(Math.pow(X,2)) -8*X + 4); System.out.println("When X is at " + X + " the value is " + xVal );
    double trigVal = (30 * (Math.PI/180));
    double sinVal = Math.sin(trigVal);
    double cosVal = Math.sin(trigVal);
    double trigSquare = (Math.pow(sinVal, 2) * Math.pow(cosVal, 2));
    System.out.println("sine: " + sinVal + " cosine: " + cosVal + " sum: " + trigSquare );
    }
 }

 class day4_a
 {
     public static void main (String[] args) throws IOException {
        BufferedReader stdin = new BufferedReader (new InputStreamReader(System.in));
        System.out.println("Enter Radius: ");
        String radius = stdin.readLine();
        int radNum = Integer.parseInt(radius);
        System.out.println("Area is: " + (Math.PI*(Math.pow(radNum, 2))));
     }
 }
 class day4_b {
    public static void main (String[] args) throws IOException {
        BufferedReader stdin = new BufferedReader (new InputStreamReader(System.in));
        System.out.println("Enter Cents: ");
        String cents = stdin.readLine();
        int centNum = Integer.parseInt(cents);
        System.out.println("You have " + (centNum / 100) + " dollars and " + (centNum % 100) + " cents");
    }
 }