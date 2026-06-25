export const TripStatus = {
  Planning: 0,
  Active: 1,
  Completed: 2,
  Cancelled: 3
} as const;

export type TripStatusType = typeof TripStatus[keyof typeof TripStatus];

export interface Destination {
  name: string;
  country: string;
}

export interface Trip {
  id: number;
  name: string;
  destination: Destination;
  startDate: string;
  endDate: string;
  status: TripStatusType;
  budget: number;
  totalExpenses?: number;
}