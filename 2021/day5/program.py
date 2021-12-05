from bresenham import bresenham
import collections

test = True
file1 = open('input-test.txt' if test else "input.txt", 'r')
lines = file1.readlines()

# empty dictionary for storing the results
dict_xy = collections.defaultdict(list)
dict_all = collections.defaultdict(list)

# Strips the newline character
for line in lines:
    # Get the coordinates
    line = line.strip().split('->')
    # Convert to x and y integers
    xy1 = tuple(int(el) for el in line[0].split(','))
    xy2 = tuple(int(el) for el in line[1].split(','))

    # Use bresenham for drawing the line (https://pypi.org/project/bresenham/)
    xys = list(bresenham(xy1[0], xy1[1], xy2[0], xy2[1]))
    # Add result to dictionary
    for xy in xys:
        # but take only horizontal and vertical in account for now in task 1
        if (xy1[0] == xy2[0] or xy1[1] == xy2[1]):
            count = 1
            if xy in dict_xy:
                count = dict_xy[xy]
                count += 1
            dict_xy[xy] = count
        # for task 2 include also diagonal lines
        count = 1
        if xy in dict_all:
            count = dict_all[xy]
            count += 1
        dict_all[xy] = count

#Check results
print("Task 1: Only horizontal and verticals: ")
count = 0
for key in dict_xy:
    if (dict_xy[key] > 1):
        # Print only in test mode as real-input has too many lines
        if test:
            print(" Coordinate {} has {} cross-overs".format(key, dict_xy[key]))
        count += 1
print(" Total count of cross-overs {}".format(count))

print("Task 2: Diagonal, horizontal and verticals: ")
count = 0
for key in dict_all:
    if (dict_all[key] > 1):       
        if test:
            print(" Coordinate {} has {} cross-overs".format(key, dict_all[key]))
        count += 1
print(" Total count of cross-overs {}".format(count))