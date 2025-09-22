• A paragraph documenting issues you encountered in the design or implementation of your chosen data structure/algorithm.


• Briefly explain the strengths and weaknesses of your data structure or algorithm with respect to resource consumption. Under what conditions does it perform the best or worst?
The strengths include Low Time Overhead(Enqueue and dequeue operations are typically O(1) in linked list or circular buffer implementations) and Predictable Memory Usage(Space grows linearly with the number of elements (O(n)), making queues easy to manage in bounded systems).
The Weaknesses include Memory Waste in Arrays(If implemented with a simple array, frequent dequeues can leave unused slots unless managed with shifting or circular logic) and Pointer Overhead in Linked Lists(Each node stores extra metadata (like pointers), which increases memory usage slightly).
The queues perform best when used for orderly, time-sensitive processing with controlled input rates. They struggle when misused for random access or when left unbounded in high-throughput systems.

• List a real-world application of your chosen data structure or algorithm.
In modern operating systems, a print queue manages multiple print jobs sent to a printer. When several users or applications request printing at the same time, the system places each job into a queue, ensuring that documents are printed in the order they were received—following the First-In, First-Out (FIFO) principle. 

• A sentence or two on the worst-case time and space complexity of your chosen data structure/algorithm.
In the worst case, a queue implemented with a dynamic array may require O(n) time for enqueue due to resizing, while dequeue remains O(1) if optimized. Space complexity is O(n), where n is the number of elements stored—this includes overhead for pointers in linked list implementations or unused slots in circular buffers.


More information about the mini project, please refer to Readme.pdf.